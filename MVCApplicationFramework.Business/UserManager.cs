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

        public ICollection<Entities.User> GetUserList()
        {
            using (MVCApplicationFrameworkDatabase _database = new MVCApplicationFrameworkDatabase())
            {
                var userList = _database.Users.Select(x=>
                 new Entities.User
                 {
                     ID = x.ID,
                     LoginName = x.LoginName,
                     ContactNumber = x.ContactNumber,
                     CreatedBy = x.CreatedBy,
                     CreatedDate = x.CreatedDate,
                     FirstName = x.FirstName,
                     LastName = x.LastName,
                     MiddleName = x.MiddleName,
                     Password = x.Password,
                     UpdatedBy = x.UpdatedBy,
                     UpdatedDate = x.UpdatedDate,
                     Roles = x.UserRoles.Select(ur => new Entities.Role
                     {
                         ID = ur.Role.ID,
                         Name = ur.Role.Name,
                         CreatedBy = ur.CreatedBy,
                         CreatedDate = ur.CreatedDate,
                         UpdatedBy = ur.UpdatedBy,
                         UpdatedDate = ur.UpdatedDate
                     }).ToList()
                 }
                ).ToList();

                return userList;
            }
        }

        public int Save(Entities.User user)
        {
            bool newRecord = true;

            using (MVCApplicationFrameworkDatabase _database = new MVCApplicationFrameworkDatabase())
            {
                var objUser = _database.Users.Where(x => x.ID == user.ID).FirstOrDefault();
                if (objUser == null)
                {
                    objUser = new DataAccess.User();
                }
                else
                {
                    objUser.UserRoles.Clear();
                    newRecord = true;
                }

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

                foreach(Entities.Role role in user.Roles)
                {
                    var objUserRole = new DataAccess.UserRole
                    {
                        RoleID = role.ID,
                        UserID = objUser.ID,
                        CreatedBy = user.CreatedBy,
                        CreatedDate = user.CreatedDate
                    };

                    _database.Entry(objUserRole).State = EntityState.Added;
                }
               
                
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
