In this Sample we have rendered the GUID type column in the Grid and you can also perform filtering with GUID column with equal operator.

````

public static List<Orders> order = new List<Orders>();
public IActionResult Index()
{
  if (order.Count == 0)
            BindDataSource();
  ViewBag.datasource = order;
  return View();
}

private static void BindDataSource()
{
  int code = 10000;

  
    order.Add(new Orders(**new Guid**("7b3bb973-6ec1-4374-9038-ffd6c6d55a50"),code + 1, "LOFKI", 1 + 0, 2.3 * 1, new DateTime(1991, 05, 15), "Berlin"));
    order.Add(new Orders(**new Guid**("8be33eb2-cff6-4871-a7c3-f1a233d70184"), code + 2, "ANATR", 1 + 2, 3.3 * 1, new DateTime(2017, 08, 11), "Madrid"));
  

  
}

public class Orders
{
  public Orders(**Guid id**, long OrderId, string CustomerId, int EmployeeId, double Freight, DateTime? OrderDate, string ShipCity)
  {
    this.id = id;
    this.OrderID = OrderId;
    this.CustomerID = CustomerId;
    this.EmployeeID = EmployeeId;
    this.Freight = Freight;
    this.OrderDate = OrderDate;
    this.ShipCity = ShipCity;
  }
  public **Guid id ** { get; set; }
  public long OrderID { get; set; }
  public string CustomerID { get; set; }
  public int EmployeeID { get; set; }
  public double Freight { get; set; }
  public DateTime? OrderDate { get; set; }
  public string ShipCity { get; set; }
}
````
