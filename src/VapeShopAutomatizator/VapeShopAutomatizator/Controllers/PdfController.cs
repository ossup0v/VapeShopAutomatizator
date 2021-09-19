using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VapeShopAutomatizator.Models;

namespace VapeShopAutomatizator.Controllers
{
	public class PdfController : Controller
	{
		private readonly PdfDbContext _context;
		private IWebHostEnvironment _hostingEnvironment;

		public PdfController(PdfDbContext context, IWebHostEnvironment environment)
		{
			_hostingEnvironment = environment;
			_context = context;
		}

		// GET: Image
		public async Task<IActionResult> Index()
		{
			return View(_context.Pdfs);
		}

		// GET: Image/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var PdfModel = _context.Pdfs.FirstOrDefault(m => m.PdfKey == id);
			if (PdfModel == null)
			{
				return NotFound();
			}

			return View(PdfModel);
		}

		// GET: Image/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Image/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("PdfKey,PdfName,PdfFile")] PdfModel PdfModel)
		{
			if (ModelState.IsValid)
			{
				//_context.Add(PdfModel);
				//await _context.SaveChangesAsync();
				if (PdfModel.PdfFile.ContentType != "application/pdf")
				{
					return RedirectToAction(nameof(Index));
				}

				if (PdfModel.PdfFile.Length > 0)
				{
					var extension = Path.GetExtension(PdfModel.PdfFile.FileName);
					var storePath = Path.Combine(_hostingEnvironment.WebRootPath, "Pdfs");
					var filePath = Path.Combine(storePath, PdfModel.PdfName + DateTime.UtcNow.ToString("yymmssfff") + extension);

					using (Stream fileStream = new FileStream(filePath, FileMode.Create))
					{
						await PdfModel.PdfFile.CopyToAsync(fileStream);
					}
				}

				_context.Pdfs.Add(PdfModel);
				return RedirectToAction(nameof(Index));
			}
			return View(PdfModel);
		}

		// GET: Image/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var PdfModel = _context.Pdfs.FirstOrDefault(o => o.PdfKey == id);
			if (PdfModel == null)
			{
				return NotFound();
			}
			return View(PdfModel);
		}

		// POST: Image/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("PdfKey,PdfName,PdfFile")] PdfModel PdfModel)
		{
			if (id != PdfModel.PdfKey)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Pdfs
						.FirstOrDefault(x => x.PdfKey == PdfModel.PdfKey)
						.Update(PdfModel);
					//_context.Update(PdfModel);
					//await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PdfModelExists(PdfModel.PdfKey))
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
			return View(PdfModel);
		}

		// GET: Image/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var PdfModel = _context.Pdfs.FirstOrDefault(m => m.PdfKey == id);
			if (PdfModel == null)
			{
				return NotFound();
			}

			return View(PdfModel);
		}

		// POST: Image/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var PdfModel = _context.Pdfs.RemoveAll(o => o.PdfKey == id);
			return RedirectToAction(nameof(Index));
		}

		private bool PdfModelExists(int id)
		{
			return _context.Pdfs.Any(e => e.PdfKey == id);
		}
	}
}
