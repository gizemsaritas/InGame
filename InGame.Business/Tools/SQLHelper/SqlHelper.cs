namespace InGame.Business.Tools.SQLHelper
{
    public static class SqlHelper
    {
        public static string GetCategoryWithParentId()
        {
            var query = "WITH cte_org AS ( SELECT " +
                        "Id,Name,ParentCategoryId FROM Category.Category " +
                        "WHERE ParentCategoryId IS NULL or ParentCategoryId = 0" +
                        "UNION ALL SELECT e.Id, e.Name,e.ParentCategoryId FROM" +
                        "Category.Category e INNER JOIN cte_org o ON o.Id = e.ParentCategoryId)" +
                        "SELECT * FROM cte_org";

            return query;

        }

        public static string GetProductWithCategory(string whereClause)
        {
            var query = "select p.Name as ProductName,c.Name as CategoryName from Product.Product p"+
            "join Category.Category c on p.CategoryId = c.Id"+
            $"{whereClause}";
            return query;
        }
    }
}
