namespace Entities.Security
{
    public class Permissions
{
    public static class Category
    {
        public const string View   = "Category.View";
        public const string Create = "Category.Create";
        public const string Edit   = "Category.Edit";
        public const string Delete = "Category.Delete";
    }

    public static class Product
    {
        public const string View   = "Product.View";
        public const string Create = "Product.Create";
        public const string Edit   = "Product.Edit";
        public const string Delete = "Product.Delete";
    }

    public static class Inventory
    {
        public const string View          = "Inventory.View";
        public const string RegisterEntry = "Inventory.RegisterEntry";
        public const string RegisterExit  = "Inventory.RegisterExit";
    }

    public static class Order
    {
        public const string View    = "Order.View";
        public const string Create  = "Order.Create";
        public const string Approve = "Order.Approve";
        public const string Reject  = "Order.Reject";
        public const string ViewAll = "Order.ViewAll";
    }

    public static class Personnel
    {
        public const string View   = "Personnel.View";
        public const string Create = "Personnel.Create";
        public const string Edit   = "Personnel.Edit";
        public const string Delete = "Personnel.Delete";
    }

    public static class Report
    {
        public const string Inventory   = "Report.Inventory";
        public const string Transaction = "Report.Transaction";
        public const string Personnel   = "Report.Personnel";
        public const string NotReceived = "Report.NotReceived";
    }

    public static IEnumerable<string> GetAll() =>
        typeof(Permissions)
            .GetNestedTypes()
            .SelectMany(t => t.GetFields())
            .Select(f => f.GetValue(null)!.ToString()!);
}
}