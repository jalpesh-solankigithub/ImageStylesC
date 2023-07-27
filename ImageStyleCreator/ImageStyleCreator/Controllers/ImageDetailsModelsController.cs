//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using ImageStyleCreator;
//using ImageStyleCreator.Models;

//namespace ImageStyleCreator.Controllers
//{
//    public class ImageDetailsModelsController : Controller
//    {
//        private readonly ImageStyleCreatorContext _context;

//        public ImageDetailsModelsController(ImageStyleCreatorContext context)
//        {
//            _context = context;
//        }

//        // GET: ImageDetailsModels
//        public async Task<IActionResult> Index()
//        {
//              return _context.ImageDetailsModel != null ? 
//                          View(await _context.ImageDetailsModel.ToListAsync()) :
//                          Problem("Entity set 'ImageStyleCreatorContext.ImageDetailsModel'  is null.");
//        }

//        // GET: ImageDetailsModels/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null || _context.ImageDetailsModel == null)
//            {
//                return NotFound();
//            }

//            var imageDetailsModel = await _context.ImageDetailsModel
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (imageDetailsModel == null)
//            {
//                return NotFound();
//            }

//            return View(imageDetailsModel);
//        }

//        // GET: ImageDetailsModels/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: ImageDetailsModels/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,Name,Size,Type,BlobUrl")] ImageDetailsModel imageDetailsModel)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(imageDetailsModel);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(imageDetailsModel);
//        }

//        // GET: ImageDetailsModels/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null || _context.ImageDetailsModel == null)
//            {
//                return NotFound();
//            }

//            var imageDetailsModel = await _context.ImageDetailsModel.FindAsync(id);
//            if (imageDetailsModel == null)
//            {
//                return NotFound();
//            }
//            return View(imageDetailsModel);
//        }

//        // POST: ImageDetailsModels/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Size,Type,BlobUrl")] ImageDetailsModel imageDetailsModel)
//        {
//            if (id != imageDetailsModel.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(imageDetailsModel);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!ImageDetailsModelExists(imageDetailsModel.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(imageDetailsModel);
//        }

//        // GET: ImageDetailsModels/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null || _context.ImageDetailsModel == null)
//            {
//                return NotFound();
//            }

//            var imageDetailsModel = await _context.ImageDetailsModel
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (imageDetailsModel == null)
//            {
//                return NotFound();
//            }

//            return View(imageDetailsModel);
//        }

//        // POST: ImageDetailsModels/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            if (_context.ImageDetailsModel == null)
//            {
//                return Problem("Entity set 'ImageStyleCreatorContext.ImageDetailsModel'  is null.");
//            }
//            var imageDetailsModel = await _context.ImageDetailsModel.FindAsync(id);
//            if (imageDetailsModel != null)
//            {
//                _context.ImageDetailsModel.Remove(imageDetailsModel);
//            }
            
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool ImageDetailsModelExists(int id)
//        {
//          return (_context.ImageDetailsModel?.Any(e => e.Id == id)).GetValueOrDefault();
//        }
//    }
//}
