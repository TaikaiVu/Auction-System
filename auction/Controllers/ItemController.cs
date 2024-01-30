using assignment1web.Models;
using assignment1web.Models.Data;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Web;
using Microsoft.AspNetCore.Authorization;


namespace assignment1web.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ItemController : Controller
    {

        private readonly IWebHostEnvironment webHostEnvironment;


        private ClientContext _context { get; set; }
        public ItemController(ClientContext context, IWebHostEnvironment webHostEnvironment)
        {
            this._context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var items = _context.Items.ToList();
            List<ItemViewModel> itemList = new List<ItemViewModel>();


            if (items != null)
            {
                foreach (var item in items)
                {
                    var ItemViewModel = new ItemViewModel()
                    {
                        ItemId = item.ItemId,
                        ProductName = item.ProductName,
                        MinimumBid = item.MinimumBid,
                        AuctionStartDate = item.AuctionStartDate,
                        AuctionEndDate = item.AuctionEndDate,
                        Category = item.Category,
                        Productdescription = item.Productdescription,
                        AssetCondition = item.AssetCondition,
                        ImageUrl = item.ImageUrl

                    };
                    itemList.Add(ItemViewModel);
                }
                return View(itemList);

            }
            return View(itemList);
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            try
            {
                var item = _context.Items.SingleOrDefault(x => x.ItemId == id);



                if (item != null)
                {
                    var itemView = new ItemViewModel()
                    {
                        ItemId = item.ItemId,
                        ProductName = item.ProductName,
                        MinimumBid = item.MinimumBid,
                        AuctionStartDate = item.AuctionStartDate,
                        AuctionEndDate = item.AuctionEndDate,
                        Category = item.Category,
                        Productdescription = item.Productdescription,
                        AssetCondition = item.AssetCondition,
                        ImageUrl = item.ImageUrl,
                        ItemImage = item.ItemImage
                    };
                    return View(itemView);
                }
                else
                {
                    TempData["errorMessage"] = $"Item details not available with that ID: {id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }

        }


        [Authorize(Roles = "Client")]
        [HttpGet]
        public IActionResult Add()
        {
            return View(new ItemViewModel());
        }

        [Authorize(Roles = "Client")]
        [HttpPost]
        public async Task<IActionResult> Add(ItemViewModel itemData)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    if (itemData.ItemImage != null)
                    {
                        string folder = "items/";
                        folder += Guid.NewGuid().ToString() + "_" + itemData.ItemImage.FileName;
                        itemData.ImageUrl = "/" + folder;

                        string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder);

                        await itemData.ItemImage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    }


                    var newItem = new Item()
                    {
                        ItemId = itemData.ItemId,
                        ProductName = itemData.ProductName,
                        MinimumBid = itemData.MinimumBid,
                        AuctionStartDate = itemData.AuctionStartDate,
                        AuctionEndDate = itemData.AuctionEndDate,
                        Category = itemData.Category,
                        Productdescription = itemData.Productdescription,
                        AssetCondition = itemData.AssetCondition,
                        ImageUrl = itemData.ImageUrl
                    };

                    _context.Items.Add(newItem);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Item added successfully to the auction !";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "The entered data is not valid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }


        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var item = _context.Items.SingleOrDefault(x => x.ItemId == id);



                if (item != null)
                {
                    var itemView = new ItemViewModel()
                    {
                        ItemId = item.ItemId,
                        ProductName = item.ProductName,
                        MinimumBid = item.MinimumBid,
                        AuctionStartDate = item.AuctionStartDate,
                        AuctionEndDate = item.AuctionEndDate,
                        Category = item.Category,
                        Productdescription = item.Productdescription,
                        AssetCondition = item.AssetCondition,
                        ImageUrl = item.ImageUrl
                    };
                    return View(itemView);
                }
                else
                {
                    TempData["errorMessage"] = $"Item details not available with that ID: {id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(ItemViewModel model)
        {
            try
            {
                var item = _context.Items.SingleOrDefault(x => x.ItemId == model.ItemId);

                if (item == null)
                {
                    TempData["errorMessage"] = $"Item not found with ID: {model.ItemId}";
                    return RedirectToAction("Index");
                }

                item.ProductName = model.ProductName;
                item.MinimumBid = model.MinimumBid;
                item.AuctionStartDate = model.AuctionStartDate;
                item.AuctionEndDate = model.AuctionEndDate;
                item.Category = model.Category;
                item.Productdescription = model.Productdescription;
                item.AssetCondition = model.AssetCondition;
                item.Amount = model.Amount;
                item.BidderName = model.BidderName;

                if (model.ItemImage != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await model.ItemImage.CopyToAsync(ms);
                        var fileBytes = ms.ToArray();
                        item.ImageUrl = $"data:image/png;base64,{Convert.ToBase64String(fileBytes)}";
                    }
                }

                _context.Items.Update(item);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // Search Items
        [HttpGet]
        public async Task<IActionResult> Search(string sortOrder, string itemSearch, string Status, int? page)
        {
            if (itemSearch != null)
            {
                page = 1;
            }
            else
            {
                itemSearch = sortOrder;
            }

            ViewData["Getitemdetails"] = itemSearch;

            var itemquery = from x in _context.Items
                            select x;
            if (!string.IsNullOrEmpty(itemSearch))
            {
                itemquery = itemquery.Where(x => x.ProductName.Contains(itemSearch) || x.Category.Contains(itemSearch));
            }
            if (!string.IsNullOrEmpty(Status))
            {
                itemquery = itemquery.Where(x => x.Productdescription.Contains(Status));
            }

            // Sorting

            ViewData["CategorySortParam"] = String.IsNullOrEmpty(sortOrder) ? "category_sort" : "";
            ViewData["PriceSortParam"] = String.IsNullOrEmpty(sortOrder) ? "price_sort" : "";
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_sort" : "";
            ViewData["CondSortParam"] = String.IsNullOrEmpty(sortOrder) ? "cond_sort" : "";
         
            switch (sortOrder)
            {
                case "category_sort":
                default:
                    itemquery = itemquery.OrderBy(x => x.Category);
                    break;
                case "price_sort":
                    itemquery = itemquery.OrderBy(x => x.MinimumBid);
                    break;
                case "name_sort":
                    itemquery = itemquery.OrderBy(x => x.ProductName);
                    break;
                case "cond_sort":
                    itemquery = itemquery.OrderBy(x => x.AssetCondition);
                    break;
            }

            // Go through Pages
            // int pageSize = 5;
            // int pageNumber = (page ?? 1);
            // return View(itemquery.ToPagedList(pageNumber, pageSize));

            return View();
        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var item = _context.Items.SingleOrDefault(x => x.ItemId == id);



                if (item != null)
                {
                    var itemView = new ItemViewModel()
                    {
                        ItemId = item.ItemId,
                        ProductName = item.ProductName,
                        MinimumBid = item.MinimumBid,
                        AuctionStartDate = item.AuctionStartDate,
                        AuctionEndDate = item.AuctionEndDate,
                        Category = item.Category,
                        Productdescription = item.Productdescription,
                        AssetCondition = item.AssetCondition,
                        ImageUrl = item.ImageUrl,
                        ItemImage = item.ItemImage
                    };
                    return View(itemView);
                }
                else
                {
                    TempData["errorMessage"] = $"Item not available with that ID: {id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }

        }

        [HttpPost]

        public IActionResult Delete(ItemViewModel model)
        {
            try
            {
                var item = _context.Items.SingleOrDefault(x => x.ItemId == model.ItemId);

                if (item != null)
                {
                    _context.Items.Remove(item);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Item deleted successfully!";
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["errorMessage"] = $"Item details not available with that ID: {model.ItemId}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [Authorize(Roles = "Client")]
        [HttpGet]
        public IActionResult Bid(int id)
        {
            try
            {
                var item = _context.Items.SingleOrDefault(x => x.ItemId == id);



                if (item != null)
                {
                    var itemView = new ItemViewModel()
                    {
                        ItemId = item.ItemId,
                        ProductName = item.ProductName,
                        MinimumBid = item.MinimumBid,
                        AuctionStartDate = item.AuctionStartDate,
                        AuctionEndDate = item.AuctionEndDate,
                        Category = item.Category,
                        Productdescription = item.Productdescription,
                        AssetCondition = item.AssetCondition,
                        ImageUrl = item.ImageUrl,
                        ItemImage = item.ItemImage,
                        Amount = item.Amount,
                        BidderName = item.BidderName

                    };
                    return View(itemView);
                }
                else
                {
                    TempData["errorMessage"] = $"Item details not available with that ID: {id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Client")]
        [HttpPost]
        public async Task<IActionResult> Bid(ItemViewModel model)
        {
            try
            {
                var item = _context.Items.SingleOrDefault(x => x.ItemId == model.ItemId);

                if (item == null)
                {
                    TempData["errorMessage"] = $"Item not found with ID: {model.ItemId}";
                    return RedirectToAction("Index");
                }

                item.ProductName = model.ProductName;
                item.MinimumBid = model.MinimumBid;
                item.AuctionStartDate = model.AuctionStartDate;
                item.AuctionEndDate = model.AuctionEndDate;
                item.Category = model.Category;
                item.Productdescription = model.Productdescription;
                item.AssetCondition = model.AssetCondition;
                item.Amount = model.Amount;
                item.BidderName = model.BidderName;

                if (model.ItemImage != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await model.ItemImage.CopyToAsync(ms);
                        var fileBytes = ms.ToArray();
                        item.ImageUrl = $"data:image/png;base64,{Convert.ToBase64String(fileBytes)}";
                    }
                }

                _context.Items.Update(item);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
 }