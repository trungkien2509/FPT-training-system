using System.Linq;

namespace FPT_Training_System.Data.Models
{
    public class TrainingProgramManagerDbContextNoTracking : TrainingProgramManagerDbContext
    {
        public TrainingProgramManagerDbContextNoTracking()
            : base()
        {
            Configuration.AutoDetectChangesEnabled = false;
        }
    }
}