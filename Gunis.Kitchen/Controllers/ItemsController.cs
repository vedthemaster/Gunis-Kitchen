using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gunis.Kitchen.Data;
using Gunis.Kitchen.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Gunis.Kitchen.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;

namespace Gunis.Kitchen.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class ItemsController : Controller
    {
        private const string BlobContainerNAME = "productimages";
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ItemsController(ApplicationDbContext context,IWebHostEnvironment hostEnvironment,IConfiguration config)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
            _config = config;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Items.Include(i => i.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,ItemPrice,ItemSize,UnitOfMeasure,ItemPhoto,CategoryID")] ItemViewModel itemViewModel)
        {
            if (ModelState.IsValid)
            {
      
                string photoUrl = null;
                if (itemViewModel.ItemPhoto != null)
                {
                    // Upload the product image to the Blob Storage Account.
                    photoUrl = await SavePhotoToBlobAsync(itemViewModel.ItemPhoto);
                }

                Item newItem = new Item
                {
                    ItemId = itemViewModel.ItemId,
                    CategoryID = itemViewModel.CategoryID,
                    ItemName = itemViewModel.ItemName,
                    ItemPrice = itemViewModel.ItemPrice,
                    UnitOfMeasure = itemViewModel.UnitOfMeasure,
                    ItemSize = itemViewModel.ItemSize,
                    ItemImageFileUrl = photoUrl,
                    ItemImageContentType = photoUrl == null ? null : itemViewModel.ItemPhoto.ContentType

                };
                _context.Add(newItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", itemViewModel.CategoryID);
            return View(itemViewModel);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var itemViewModel = new ItemViewModel
            {
                ItemId = item.ItemId,
                CategoryID = item.CategoryID,
                ItemName = item.ItemName,
                ItemPrice = item.ItemPrice,
                UnitOfMeasure = item.UnitOfMeasure,
                ItemSize = item.ItemSize,
            };

            ViewBag.ItemImageFileUrl = item.ItemImageFileUrl;
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", itemViewModel.CategoryID);
            return View(itemViewModel);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,UnitOfMeasure,ItemPrice,ItemSize,ItemPhoto,CategoryID")] ItemViewModel itemViewModel)
        {
            if (id != itemViewModel.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                string photoUrl = null;
                if (itemViewModel.ItemPhoto != null)
                {
                    // Upload the product image to the Blob Storage Account.
                    photoUrl = await SavePhotoToBlobAsync(itemViewModel.ItemPhoto);
                }

                var item = _context.Items.SingleOrDefault(p => p.ItemId == itemViewModel.ItemId);
                item.CategoryID = itemViewModel.CategoryID;
                item.ItemName = itemViewModel.ItemName;
                item.ItemPrice = itemViewModel.ItemPrice;
                item.UnitOfMeasure = itemViewModel.UnitOfMeasure;
                item.ItemSize = itemViewModel.ItemSize;

                if (photoUrl != null)
                {
                    item.ItemImageFileUrl = photoUrl;
                    item.ItemImageContentType = itemViewModel.ItemPhoto.ContentType;
                }



                try
                {

                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", itemViewModel.CategoryID);
            return View(itemViewModel);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }

        private async Task<string> SavePhotoToBlobAsync(IFormFile productImage)
        {
            string storageConnection1 = _config.GetValue<string>("MyAzureSettings:StorageAccountKey1");
            string storageConnection2 = _config.GetValue<string>("MyAzureSettings:StorageAccountKey2");
            string fileName = productImage.FileName;
            string tempFilePath = string.Empty;
            string photoUrl;

            if (productImage != null && productImage.Length > 0)
            {
                // Save the uploaded file on to a TEMP file.
                tempFilePath = Path.GetTempFileName();
                using (var stream = System.IO.File.Create(tempFilePath))
                {
                    productImage.CopyToAsync(stream).Wait();
                }
            }

            // Get a reference to a container 
            BlobContainerClient blobContainerClient = new BlobContainerClient(storageConnection1, BlobContainerNAME);

            // Create the container if it does not exist - granting PUBLIC access.
            await blobContainerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);

            // Create the client to the Blob Item
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

            // Open the file and upload its data
            using (FileStream uploadFileStream = System.IO.File.OpenRead(tempFilePath))
            {
                await blobClient.UploadAsync(uploadFileStream, overwrite: true);
                uploadFileStream.Close();
            }

            // Delete the TEMP file since it is no longer needed.
            System.IO.File.Delete(tempFilePath);

            // Return the URI of the item in the Blob Storage
            photoUrl = blobClient.Uri.ToString();
            return photoUrl;
        }

    }
}
