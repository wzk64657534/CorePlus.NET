using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core;
using CorePlus.Entity;
using CorePlus.Common;

namespace CorePlus.Repository
{
    public class RoleRepository : SimpleRepository<RoleInfoEntity>
    {
        public List<MenuInfoEntity> GetCurrentRoleMenu(long id)
        {
            var menuAll = (from x in this.DB.Set<MenuInfoEntity>()
                           where x.IsToUser == true
                           select x).ToList();

            var query = (from x in this.DB.Set<RoleMenuInfoEntity>()
                         where x.RoleId == id
                         select x).ToList();

            if (query != null)
            {
                foreach (var item in query)
                {
                    foreach (var menu in menuAll)
                    {
                        if (menu.ID == item.MenuId) { menu.Selected = true; break; }
                    }
                }
            }

            return menuAll;
        }

        public bool SetRoleMenu(RoleInfoEntity entity, long[] menuIds)
        {
            base.Add(entity);
            if (entity.ID > 0)
            {
                RoleMenuInfoRepository repository = new RoleMenuInfoRepository();
                foreach (var menuId in menuIds)
                {
                    RoleMenuInfoEntity rmEntity = new RoleMenuInfoEntity();
                    rmEntity.RoleId = entity.ID;
                    rmEntity.MenuId = menuId;
                    repository.Add(rmEntity);
                }

                return true;
            }

            return false;
        }

        public bool UpdateRoleMenu(RoleInfoEntity entity, long[] menuIds)
        {
            try
            {
                base.Update(entity.ID, entity);

                this.DB.Delete<RoleMenuInfoEntity>(x => x.RoleId == entity.ID);
                if (menuIds != null)
                {
                    RoleMenuInfoRepository repository = new RoleMenuInfoRepository();
                    foreach (var menuId in menuIds)
                    {
                        RoleMenuInfoEntity rmEntity = new RoleMenuInfoEntity();
                        rmEntity.RoleId = entity.ID;
                        rmEntity.MenuId = menuId;
                        repository.Add(rmEntity);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsAdministrator(long id)
        {
            var entity = FindByID(id);
            return entity != null && entity.RoleName.Contains(ConstCommonHelper.RoleAdministratorForShort);
        }
    }
}