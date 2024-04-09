using System.Collections;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.EJ2.Base;

namespace CoreGridApp.Controllers
{
  public class HomeController : Controller
  {
    public static List<Orders> order = new List<Orders>();
    public IActionResult Index()
    {
      if (order.Count == 0)
                BindDataSource();
      ViewBag.datasource = order;
      return View();
    }
    public IActionResult UrlDatasource([FromBody] DataManagerRequest dm)
    {
      var gridData = order;
      IEnumerable DataSource = gridData;
      DataOperations operation = new DataOperations();
      if (dm.Search != null && dm.Search.Count > 0)
      {
        DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
      }
      if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
      {
        DataSource = operation.PerformSorting(DataSource, dm.Sorted);
      }
      if (dm.Where != null && dm.Where.Count > 0) //Filtering
      {
        DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
      }
      int count = DataSource.Cast<Orders>().Count();
      if (dm.Skip != 0)
      {
        DataSource = operation.PerformSkip(DataSource, dm.Skip); //Paging
      }
      if (dm.Take != 0)
      {
        DataSource = operation.PerformTake(DataSource, dm.Take);
      }
      return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
    }

    public IActionResult Insert([FromBody] CRUDModel<Orders> value)
    {
      order.Insert(0, value.Value);
      return Json(value.Value);
    }
    public IActionResult Update([FromBody] CRUDModel<Orders> value)
    {
      var data = order.Where(or => or.OrderID == value.Value.OrderID).FirstOrDefault();
      if (data != null)
      {
        data.OrderID = value.Value.OrderID;
        data.CustomerID = value.Value.CustomerID;
        data.EmployeeID = value.Value.EmployeeID;
        data.OrderDate = value.Value.OrderDate;
        data.ShipCity = value.Value.ShipCity;
        data.Freight = value.Value.Freight;
      }
      return Json(value.Value);
    }
    public void Remove([FromBody] CRUDModel<Orders> Value)
    {
      var data = order.First(or => or.OrderID == ((JsonElement)Value.Key).GetInt64());
      order.Remove(data);
    }
    private static void BindDataSource()
    {
      int code = 10000;

      
        order.Add(new Orders(new Guid("7b3bb973-6ec1-4374-9038-ffd6c6d55a50"),code + 1, "LOFKI", 1 + 0, 2.3 * 1, new DateTime(1991, 05, 15), "Berlin"));
        order.Add(new Orders(new Guid("8be33eb2-cff6-4871-a7c3-f1a233d70184"), code + 2, "ANATR", 1 + 2, 3.3 * 1, new DateTime(2017, 08, 11), "Madrid"));
      

      
    }
    public class Orders
    {
      public Orders(Guid id, long OrderId, string CustomerId, int EmployeeId, double Freight, DateTime? OrderDate, string ShipCity)
      {
                this.id = id;
        this.OrderID = OrderId;
        this.CustomerID = CustomerId;
        this.EmployeeID = EmployeeId;
        this.Freight = Freight;
        this.OrderDate = OrderDate;
        this.ShipCity = ShipCity;
      }
        public Guid id { get; set; }
      public long OrderID { get; set; }
      public string CustomerID { get; set; }
      public int EmployeeID { get; set; }
      public double Freight { get; set; }
      public DateTime? OrderDate { get; set; }
      public string ShipCity { get; set; }
    }

    public IActionResult Error()
    {
      ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
      return View();
    }
  }
}
