using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MVCApplicationFramework.DataAccess;
using MVCApplicationFramework.Entities;
using System.Collections;

namespace MVCApplicationFramework.Business
{
    public class UserManager
    {
       

        public Entities.User GetUserByID(int userId)
        {
            using (MVCApplicationFrameworkDatabase _database = new MVCApplicationFrameworkDatabase())
            {
                var user = _database.Users.Where(x => x.ID == userId).FirstOrDefault();

                Entities.User objUser = new Entities.User
                {
                    ID = user.ID,
                    LoginName = user.LoginName,
                    ContactNumber = user.ContactNumber,
                    CreatedBy = user.CreatedBy,
                    CreatedDate = user.CreatedDate,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MiddleName = user.MiddleName,
                    Password = user.Password,
                    UpdatedBy = user.UpdatedBy,
                    UpdatedDate = user.UpdatedDate,
                    Roles = user.UserRoles.Select(x => new Entities.Role
                    {
                        ID = x.Role.ID,
                        Name = x.Role.Name,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate
                    }).ToList()
                };
                return objUser;
            }
        }

        public int Save(Entities.User user)
        {
            bool newRecord = true;

            using (MVCApplicationFrameworkDatabase _database = new MVCApplicationFrameworkDatabase())
            {
                var objUser = _database.Users.Where(x => x.ID == user.ID).FirstOrDefault();

                objUser.ID = user.ID;
                objUser.LoginName = user.LoginName;
                objUser.ContactNumber = user.ContactNumber;
                objUser.CreatedBy = user.CreatedBy;
                objUser.CreatedDate = user.CreatedDate;
                objUser.FirstName = user.FirstName;
                objUser.LastName = user.LastName;
                objUser.MiddleName = user.MiddleName;
                objUser.Password = user.Password;
                objUser.UpdatedBy = user.UpdatedBy;
                objUser.UpdatedDate = user.UpdatedDate;

                if (objUser == null)
                {
                    objUser = new DataAccess.User();
                    newRecord = false;
                }
                else
                {
                    objUser.UserRoles.Clear();
                }

                objUser.UserRoles = user.Roles.Select(r => new DataAccess.UserRole
                {
                    UserID = objUser.ID,
                    RoleID = r.ID,
                    CreatedBy = user.CreatedBy
                }).ToList();

                if (newRecord)
                {
                    _database.Entry(objUser).State = EntityState.Added;
                }
                else
                {
                    _database.Entry(objUser).State = EntityState.Modified;
                }

                _database.SaveChanges();

                return objUser.ID;
            }
        }
    }
}
