using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class MealType : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public double? MealCost { get; set; }
        public double? ClientInvoiceAmount { get; set; }
    }

    class MealTypeConfig : IEntityTypeConfiguration<MealType>
    {
        public void Configure(EntityTypeBuilder<MealType> entity)
        {
            entity.HasData(
                new MealType { Id = 1, Code = "1 a day", Description = "1 trays 1 bags (Low Cholesterol/Low Sodium Meal)", MealCost = 9.70, ClientInvoiceAmount = 14.10, DisplayOrder = 1, Enabled = true },
                new MealType { Id = 2, Code = "2 a Day Reg", Description = "2 trays - 2 bags daily regular", DisplayOrder = 2, Enabled = true },
                new MealType { Id = 3, Code = "2 skim, spice, caf", Description = "2 Skim Milk Only, No Spicy Food, No Caffeine", MealCost = 4.85, ClientInvoiceAmount = 7.05, DisplayOrder = 3, Enabled = true },
                new MealType { Id = 4, Code = "Friday Special", Description = "3 trays, 3 bags, 3 supper bags ON FRIDAYS diabetic low sodium", MealCost = 18.90, ClientInvoiceAmount = 25.80, DisplayOrder = 4, Enabled = true },
                new MealType { Id = 5, Code = "Friday Special #1", Description = "2 Trays 2 Bags (Friday Only) regular", MealCost = 4.50, ClientInvoiceAmount = 12.10, DisplayOrder = 5, Enabled = true },
                new MealType { Id = 6, Code = "Friday Special #2", Description = "1 Tray -1 Bag (Friday Fish Sub)", MealCost = 4.85, ClientInvoiceAmount = 7.05, DisplayOrder = 6, Enabled = true },
                new MealType { Id = 7, Code = "Friday Special #3", Description = "3 trays, 3 bags Diabetic (Friday Only)", DisplayOrder = 7, Enabled = true },
                new MealType { Id = 8, Code = "Friday Special #4", Description = "3 trays, 3 bags regular (Friday Only)", MealCost = 12.75, ClientInvoiceAmount = 18.15, DisplayOrder = 8, Enabled = true },
                new MealType { Id = 9, Code = "no Salt", Description = "NO SALT/DIABETIC", MealCost = 4.85, ClientInvoiceAmount = 7.05, DisplayOrder = 9, Enabled = true },
                new MealType { Id = 10, Code = "Regular Meal", Description = "1 tray 1 bag regular", MealCost = 4.33, ClientInvoiceAmount = 6.05, DisplayOrder = 10, Enabled = true },
                new MealType { Id = 11, Code = "S1-DiabLoCholNORice", Description = "1 Tray - 1 Bag (Diabetic, Low Cholesterol, NO RICE)", MealCost = 6.24, ClientInvoiceAmount = 7.80, DisplayOrder = 11, Enabled = true },
                new MealType { Id = 12, Code = "SA1-NO MUSHROOMS", Description = "1 Tray - 1 Bag (Special - Alergic NO MUSHROOMS)", MealCost = 6.24, ClientInvoiceAmount = 7.80, DisplayOrder = 12, Enabled = true },
                new MealType { Id = 13, Code = "Sp Veg", Description = "Special Vege", ClientInvoiceAmount = 7.80, DisplayOrder = 13, Enabled = true },
                new MealType { Id = 14, Code = "Special Lactose Free", Description = "Special Meals no milk/dairy at all", DisplayOrder = 14, Enabled = true },
                new MealType { Id = 15, Code = "Special-Diabetic", Description = "1 Tray - 1 Bag (Diabetic Meal)", MealCost = 4.85, ClientInvoiceAmount = 7.05, DisplayOrder = 15, Enabled = true },
                new MealType { Id = 16, Code = "Special-DiabLoChol", Description = "1 Tray - 1 Bag (Diabetic, Low Cholesterol)", MealCost = 4.85, ClientInvoiceAmount = 7.05, DisplayOrder = 16, Enabled = true },
                new MealType { Id = 17, Code = "Special-LoChlSdmDiab", Description = "1 Tray - 1 Bag (Diabetic, Low Cholesterol and Low Sodium Meal)", MealCost = 4.82, ClientInvoiceAmount = 7.05, DisplayOrder = 17, Enabled = true },
                new MealType { Id = 18, Code = "Special-LoChol", Description = "1 Tray - 1 Bag (Low Cholesterol Meal)", MealCost = 4.85, ClientInvoiceAmount = 7.05, DisplayOrder = 18, Enabled = true },
                new MealType { Id = 19, Code = "Special-LoCholSodm", Description = "1 Tray - 1 Bag (Low Cholesterol and Low Sodium Meal)", MealCost = 4.85, ClientInvoiceAmount = 7.05, DisplayOrder = 19, Enabled = true },
                new MealType { Id = 20, Code = "Special-LoSodm", Description = "1 Tray - 1 Bag (Low Sodium Meal)", MealCost = 4.85, ClientInvoiceAmount = 7.05, DisplayOrder = 20, Enabled = true },
                new MealType { Id = 21, Code = "Special-LoSodmDiab", Description = "1 Tray - 1 Bay (Low Sodium and Diabetic)", MealCost = 4.85, ClientInvoiceAmount = 7.05, DisplayOrder = 21, Enabled = true },
                new MealType { Id = 22, Code = "Special-Substitution", Description = "1 Tray - 1 Bag (Special Subs)", MealCost = 4.85, ClientInvoiceAmount = 7.05, DisplayOrder = 22, Enabled = true },
                new MealType { Id = 23, Code = "Supper Bag", Description = "1 Supper Bag", MealCost = 2.05, ClientInvoiceAmount = 2.55, DisplayOrder = 23, Enabled = true }
            );
        }
    }
}
