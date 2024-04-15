using DataLayer.Models;

namespace DataLayer.Repositories.Abstract;

public interface ICharityPhotoRepository
{
    public Task InsertAsync(Charity charity);
    
}