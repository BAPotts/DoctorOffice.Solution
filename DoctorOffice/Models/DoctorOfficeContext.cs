using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DoctorOffice.Models
{
  public class DoctorOfficeContext: DbContext
  {
    public virtual DbSet<Doctor> Doctors {get; set;}
    public DbSet<Patient> Patients {get; set;}
    public DbSet<DoctorPatient> DoctorPatient {get; set;}

    public DoctorOfficeContext(DbContextOptions options) :base(options) { }
  }
}