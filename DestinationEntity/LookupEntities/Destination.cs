using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSSRC_DataMigration.DestinationEntity
{
    public class Destination : BaseLookupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class DestinationConfig : IEntityTypeConfiguration<Destination>
    {
        public void Configure(EntityTypeBuilder<Destination> entity)
        {
            entity.HasData(
                new Destination { Id = 1, Name = "Aldi Foods", DisplayOrder = 1, Enabled = true },
                new Destination { Id = 2, Name = "zz Jerry's IGA", DisplayOrder = 2, Enabled = true },
                new Destination { Id = 3, Name = "County Market", DisplayOrder = 3, Enabled = true },
                new Destination { Id = 4, Name = "Schnuck's", DisplayOrder = 4, Enabled = true },
                new Destination { Id = 5, Name = "Meijer's", DisplayOrder = 5, Enabled = true },
                new Destination { Id = 6, Name = "Walmart", DisplayOrder = 6, Enabled = true },
                new Destination { Id = 7, Name = "zz Provena Covenant Medical Center", DisplayOrder = 7, Enabled = true },
                new Destination { Id = 8, Name = "zz Carle Foundation Hospital", DisplayOrder = 8, Enabled = true },
                new Destination { Id = 9, Name = "zz Illini Dental", DisplayOrder = 9, Enabled = true },
                new Destination { Id = 10, Name = "zz Dr. Wrestler & Associates", DisplayOrder = 10, Enabled = true },
                new Destination { Id = 11, Name = "zz Christie--University", DisplayOrder = 11, Enabled = true },
                new Destination { Id = 12, Name = "zz Christie-- Urbana", DisplayOrder = 12, Enabled = true },
                new Destination { Id = 13, Name = "zz Christie-- Kirby & Duncan", DisplayOrder = 13, Enabled = true },
                new Destination { Id = 14, Name = "zz Christie-- Cancer Center", DisplayOrder = 14, Enabled = true },
                new Destination { Id = 15, Name = "zz Christie-- CU Sleep", DisplayOrder = 15, Enabled = true },
                new Destination { Id = 16, Name = "zz Christie -- Windsor", DisplayOrder = 16, Enabled = true },
                new Destination { Id = 17, Name = "zz CarleClinic-- Rantoul", DisplayOrder = 17, Enabled = true },
                new Destination { Id = 18, Name = "zz Carle Clinic--Curtis Road", DisplayOrder = 18, Enabled = true },
                new Destination { Id = 19, Name = "zz Carle Clinic--Windsor, Urbana", DisplayOrder = 19, Enabled = true },
                new Destination { Id = 20, Name = "zz Carle Clinic--SE Urbana", DisplayOrder = 20, Enabled = true },
                new Destination { Id = 21, Name = "zz Carle Clinic--Urbana Main", DisplayOrder = 21, Enabled = true },
                new Destination { Id = 22, Name = "zz Carle Heart Center", DisplayOrder = 22, Enabled = true },
                new Destination { Id = 23, Name = "zz Carle Spine Institute", DisplayOrder = 23, Enabled = true },
                new Destination { Id = 24, Name = "zz Carle Cancer Center", DisplayOrder = 24, Enabled = true },
                new Destination { Id = 25, Name = "zz Carle Wound Clinic", DisplayOrder = 25, Enabled = true },
                new Destination { Id = 26, Name = "Target", DisplayOrder = 26, Enabled = true },
                new Destination { Id = 27, Name = "zz Pharmacy", DisplayOrder = 27, Enabled = true },
                new Destination { Id = 28, Name = "Bank", DisplayOrder = 28, Enabled = true },
                new Destination { Id = 29, Name = "Dentist", DisplayOrder = 29, Enabled = true },
                new Destination { Id = 30, Name = "Doctor", DisplayOrder = 30, Enabled = true },
                new Destination { Id = 31, Name = "zz Church", DisplayOrder = 31, Enabled = true },
                new Destination { Id = 32, Name = "Nursing Home", DisplayOrder = 32, Enabled = true },
                new Destination { Id = 33, Name = "zz Adult Day Care", DisplayOrder = 33, Enabled = true },
                new Destination { Id = 34, Name = "zz Library", DisplayOrder = 34, Enabled = true },
                new Destination { Id = 35, Name = "Hairdresser/Manicure", DisplayOrder = 35, Enabled = true },
                new Destination { Id = 36, Name = "Volunteer/Work", DisplayOrder = 36, Enabled = true },
                new Destination { Id = 37, Name = "Frances Nelson", DisplayOrder = 37, Enabled = true },
                new Destination { Id = 38, Name = "zz Widick Foot Clinic", DisplayOrder = 38, Enabled = true },
                new Destination { Id = 39, Name = "CU Dialysis", DisplayOrder = 39, Enabled = true },
                new Destination { Id = 40, Name = "MTD", DisplayOrder = 40, Enabled = true },
                new Destination { Id = 41, Name = "zz Carle -- Pavillion", DisplayOrder = 41, Enabled = true },
                new Destination { Id = 42, Name = "zz Provena Partners", DisplayOrder = 42, Enabled = true },
                new Destination { Id = 43, Name = "Other -- Business", DisplayOrder = 43, Enabled = true },
                new Destination { Id = 44, Name = "Other -- QOL", DisplayOrder = 44, Enabled = true },
                new Destination { Id = 45, Name = "zz Carle-- Clinic", DisplayOrder = 45, Enabled = true },
                new Destination { Id = 46, Name = "Other -- Medical", DisplayOrder = 46, Enabled = true },
                new Destination { Id = 47, Name = "zz Carle-- Windsor and Duncan", DisplayOrder = 47, Enabled = true },
                new Destination { Id = 48, Name = "Provena", DisplayOrder = 48, Enabled = true },
                new Destination { Id = 49, Name = "Carle", DisplayOrder = 49, Enabled = true },
                new Destination { Id = 50, Name = "Christie", DisplayOrder = 50, Enabled = true },
                new Destination { Id = 51, Name = "Carle - Rantoul", DisplayOrder = 51, Enabled = true },
                new Destination { Id = 52, Name = "zz Pavillion", DisplayOrder = 52, Enabled = true },
                new Destination { Id = 53, Name = "Christie - Mahomet", DisplayOrder = 53, Enabled = true },
                new Destination { Id = 54, Name = "Muscatella Family Foot", DisplayOrder = 54, Enabled = true },
                new Destination { Id = 55, Name = "zz Optical NS", DisplayOrder = 55, Enabled = true },
                new Destination { Id = 56, Name = "zz CVS", DisplayOrder = 56, Enabled = true },
                new Destination { Id = 57, Name = "Walgreens", DisplayOrder = 57, Enabled = true },
                new Destination { Id = 58, Name = "Eye Center", DisplayOrder = 58, Enabled = true },
                new Destination { Id = 59, Name = "Other - Grocery", DisplayOrder = 59, Enabled = true },
                new Destination { Id = 60, Name = "zz Champaign Dental", DisplayOrder = 60, Enabled = true },
                new Destination { Id = 61, Name = "zz Hearing NS", DisplayOrder = 61, Enabled = true },
                new Destination { Id = 62, Name = "zz Dentistry by Design", DisplayOrder = 62, Enabled = true },
                new Destination { Id = 63, Name = "Aspen Dental", DisplayOrder = 63, Enabled = true },
                new Destination { Id = 64, Name = "zz Audibel", DisplayOrder = 64, Enabled = true },
                new Destination { Id = 65, Name = "zz Busey Bank", DisplayOrder = 65, Enabled = true },
                new Destination { Id = 66, Name = "Bayview Family Clinic", DisplayOrder = 66, Enabled = true },
                new Destination { Id = 67, Name = "zz Bo Ric", DisplayOrder = 67, Enabled = true },
                new Destination { Id = 68, Name = "zz Family Dental", DisplayOrder = 68, Enabled = true },
                new Destination { Id = 69, Name = "zzHanger Orthopedic", DisplayOrder = 69, Enabled = true },
                new Destination { Id = 70, Name = "Community Elements", DisplayOrder = 70, Enabled = true },
                new Destination { Id = 71, Name = "zz Regional Planning", DisplayOrder = 71, Enabled = true },
                new Destination { Id = 72, Name = "Social Security", DisplayOrder = 72, Enabled = true },
                new Destination { Id = 73, Name = "DHS", DisplayOrder = 73, Enabled = true },
                new Destination { Id = 74, Name = "zz Retina of Illinois", DisplayOrder = 74, Enabled = true },
                new Destination { Id = 75, Name = "Davita Dialysis", DisplayOrder = 75, Enabled = true },
                new Destination { Id = 76, Name = "zz Dankle, Brunson and Lee Dentistry", DisplayOrder = 76, Enabled = true },
                new Destination { Id = 77, Name = "zz CU Dental", DisplayOrder = 77, Enabled = true },
                new Destination { Id = 78, Name = "DMV", DisplayOrder = 78, Enabled = true },
                new Destination { Id = 79, Name = "OSF", DisplayOrder = 79, Enabled = true },
                new Destination { Id = 80, Name = "Rosecrance", DisplayOrder = 80, Enabled = true }
            );
        }
    }
}