namespace FSSRC_DataMigration.DestinationEntity
{
    public class RouteInfo
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public int? DayOfWeek { get; set; }
        public int? RouteId { get; set; }
        public int? MealTypeId { get; set; }
        public string? DeliveryNumber { get; set; }
        public string? Description { get; set; }
        public int MealsOnWheelsRecordId { get; set; }
    }
}
