using Microsoft.EntityFrameworkCore;

namespace aspnet2.Models
{
    public class CarServiceContext : DbContext
    {
        public CarServiceContext(DbContextOptions<CarServiceContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(x => x.VINcode);
                entity.Property(x => x.VINcode).HasMaxLength(18);
                entity.Property(x => x.Brand).HasMaxLength(20);
                entity.Property(x => x.Model).HasMaxLength(20);
                entity.ToTable(t => t.HasCheckConstraint("YearOfProd", "YearOfProd > 2000 AND YearOfProd <= 2022"));
                entity.HasData(
                    new Car { VINcode = "19UUB7F02MA000899", Brand = "Acura", Model = "TLX", YearOfProd = 2021, Mileage = 12368 },
                    new Car { VINcode = "5J8YE1H89NL032957", Brand = "Acura", Model = "MDX", YearOfProd = 2022, Mileage = 5429 },
                    new Car { VINcode = "2T2HZMAAXNC228796", Brand = "Lexus", Model = "RX", YearOfProd = 2022, Mileage = 2315 },
                    new Car { VINcode = "WAUG8AFC1JN012500", Brand = "Audi", Model = "A6", YearOfProd = 2018, Mileage = 42156 },
                    new Car { VINcode = "WP1AA2A2XGKA08083", Brand = "Skoda", Model = "Octavia", YearOfProd = 2016, Mileage = 100344 },
                    new Car { VINcode = "WVGFF9BP8BD000455", Brand = "Volkswagen", Model = "Touareg", YearOfProd = 2014, Mileage = 156302 },
                    new Car { VINcode = "1GYFK43519R118886", Brand = "Cadillac", Model = "Escalade", YearOfProd = 2007, Mileage = 245711 }
                );
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasOne(d => d.Car).WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.CarVINcode);
                entity.HasData(
                    new Invoice { InvoiceId = 1, CarVINcode = "19UUB7F02MA000899" },
                    new Invoice { InvoiceId = 2, CarVINcode = "5J8YE1H89NL032957" },
                    new Invoice { InvoiceId = 3, CarVINcode = "WP1AA2A2XGKA08083" },
                    new Invoice { InvoiceId = 4, CarVINcode = "2T2HZMAAXNC228796" },
                    new Invoice { InvoiceId = 5, CarVINcode = "2T2HZMAAXNC228796" },
                    new Invoice { InvoiceId = 6, CarVINcode = "1GYFK43519R118886" },
                    new Invoice { InvoiceId = 7, CarVINcode = "WVGFF9BP8BD000455" },
                    new Invoice { InvoiceId = 8, CarVINcode = "19UUB7F02MA000899" }
                );
            });
        }
    }
}
