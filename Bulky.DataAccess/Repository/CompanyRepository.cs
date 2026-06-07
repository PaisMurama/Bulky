using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBookWeb.DataAccess.Data;

namespace BulkyBook.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>,ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        

        public CompanyRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;

        }
       
        public void Update(Company obj)
        {
            var objFromDb = _db.Companies.FirstOrDefault(s => s.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.PhoneNumber = obj.PhoneNumber;
                objFromDb.City = obj.City;
                objFromDb.StreetAdress = obj.StreetAdress;
                objFromDb.State = obj.State;
                objFromDb.PostalCode = obj.PostalCode;
                

            }

        }
    }
}
