using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    public class TaskController : Controller
    {
        private static List<TaskItem> _items = new();

        [HttpGet]
        public IActionResult Index(string search)
        {
            var items = _items;

            if (!string.IsNullOrEmpty(search))
            {
                items = _items.Where(x => x.Title.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return View(items);
        }

        [HttpPost]
        public IActionResult Add(string title)
        {
            if (string.IsNullOrWhiteSpace(title) || title == "Görev giriniz")
            {
                TempData["Error"] = "Lütfen geçerli bir görev giriniz!";
                return RedirectToAction("Index");
            }

            var item = new TaskItem
            {
                Id = _items.Count + 1,
                Title = title,
                IsCompleted = false,
                Date = DateTime.Now
            };
            _items.Add(item);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Complete(int id)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                item.IsCompleted = true;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            var ItemToRemove = _items.FirstOrDefault(y => y.Id == id);

            if (ItemToRemove != null)
            {
                _items.Remove(ItemToRemove);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var item = _items.FirstOrDefault(z => z.Id == id);

            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(TaskItem UpdatedItem)
        {
            var item = _items.FirstOrDefault(z => z.Id == UpdatedItem.Id);

            if (item == null)
            {
                return NotFound();
            }

            item.Title = UpdatedItem.Title;
            return RedirectToAction("Index");
        }
    }
}
