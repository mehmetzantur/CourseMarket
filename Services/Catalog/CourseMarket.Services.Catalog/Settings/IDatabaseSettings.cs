namespace CourseMarket.Services.Catalog.Settings
{
    internal interface IDatabaseSettings
    {
        public string CourseCollectionName { get; set; }
        public string CatalogCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
