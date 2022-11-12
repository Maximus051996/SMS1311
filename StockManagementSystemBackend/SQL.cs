namespace StockManagementSystemBackend
{
    public static class SQL
    {
        #region UserMaster
        public const string GetUserDetails = "Select [UserId],[UserName],[UserPassword],[Email],[ContactNumber], TM.[TenantName],us.[IsActive],ro.RoleName" +
                                         " from[dbo].[UserMaster] us inner join[dbo].[RoleMaster] ro " +
                                         "on us.RoleId=ro.RoleId and us.TenantId=ro.TenantId " +
                                         "inner join [dbo].[TenantMaster] TM on TM.TenantId=us.TenantId where UserName=@UserName";

        public const string InsertUser = "IF NOT EXISTS(Select * from [dbo].[UserMaster] where UserName=@UserName)"
                                          + "Begin INSERT INTO [dbo].[UserMaster] (RoleId,UserName,UserPassword,Email,ContactNumber, TenantId," +
                                         "Address,CreatedDateTime, IsActive) " +
                                         "VALUES (@RoleId,@UserName,@UserPassword,@Email,@ContactNumber,@TenantId,@Address,GETUTCDATE(),@IsActive)" +
                                          "Select 1 End Else Begin Select 0 End";

        public const string EnableDisableUser = @"Update [dbo].[UserMaster] Set IsActive=@IsActive,ModifiedDateTime=GETUTCDATE()
                                                  where UserId=@UserId";

        public const string UpdateUser = @"Update [dbo].[UserMaster] Set RoleId=@RoleId,Email=@Email,ContactNumber=@ContactNumber,
                                           ModifiedDateTime=GETUTCDATE(),Address=@Address
                                           where UserId=@UserId and TenantId=@TenantId";

        public const string GetUserById = @"Select [UserId],[UserName],[Email],[TenantId] from [dbo].[UserMaster] where UserId=@UserId";

        public const string GetUsers = @"Select * from [dbo].[UserMaster] where TenantId=@TenantId";
        public const string TotalUserByTenanat = @"Select TM.TenantName,count(*) TenantCount from UserMaster UM
                                                   inner join TenantMaster TM
                                                   on UM.TenantId=TM.TenantId
                                                   where Tm.IsActive=1
                                                   group by TM.TenantName order by TM.TenantName desc";

        public const string GetUsersByTenant = @"Select UserId,UserName,T.TenantName,U.IsActive,Email from UserMaster U
                                                 inner join TenantMaster T
                                                 on U.TenantId=T.TenantId";
        #endregion

        #region RoleMaster
        public const string InsertRole = "IF NOT EXISTS(Select * from [dbo].[RoleMaster] where RoleName=@RoleName and TenantId=@TenantId)"
                                         + "Begin INSERT INTO [dbo].[RoleMaster] (RoleName, TenantId,CreatedDateTime, IsActive) " +
                                         "VALUES (@RoleName, @TenantId,GETUTCDATE(), @IsActive) Select 1 End Else Begin Select 0 End";

        public const string EnableDisableRole = @"Update [dbo].[RoleMaster] Set IsActive=@IsActive,ModifiedDateTime=GETUTCDATE()
                                                  where RoleId=@RoleId and TenantId=@TenantId";

        public const string UpdateRole = @"Update [dbo].[RoleMaster] Set RoleName=@RoleName,ModifiedDateTime=GETUTCDATE()
                                           where RoleId=@RoleId and TenantId=@TenantId";

        public const string GetRoleById = @"Select * from [dbo].[RoleMaster] where RoleId=@RoleId and TenantId=@TenantId";

        public const string GetRoles = @"Select * from [dbo].[RoleMaster] where TenantId=@TenantId";

        public const string GetAllActiveRoles = @"Select Distinct RoleName from RoleMaster where IsActive=1";
        #endregion

        #region TenantMaster
        public const string InsertTenant = "IF NOT EXISTS(Select * from [dbo].[TenantMaster] where TenantName=@TenantName) " +
                         " Begin INSERT INTO [dbo].[TenantMaster] (TenantName, CreatedDateTime, IsActive) " +
                         "VALUES(@TenantName, GETUTCDATE(), @IsActive) Select 1 End Else Begin Select 0 End";

        public const string EnableDisableTenant = @"Update [dbo].[TenantMaster] Set IsActive=@IsActive,ModifiedDateTime=GETUTCDATE()
                                                    where TenantId=@TenantId";

        public const string UpdateTenant = @"Update [dbo].[TenantMaster] Set TenantName=@TenantName,ModifiedDateTime=GETUTCDATE()
                                             where TenantId=@TenantId";

        public const string GetTenantById = @"Select * from [dbo].[TenantMaster] where TenantId=@TenantId";

        public const string GetTenants = @"Select * from [dbo].[TenantMaster] order by TenantName desc";


        public const string GetRoleCountByTenant = @"Select count(RM.RoleName) RoleCount from UserMaster UM
                                                inner join RoleMaster RM
                                                on UM.RoleId=RM.RoleId
                                                inner join TenantMaster TM
                                                on TM.TenantId=UM.TenantId
                                                where RM.RoleName=@RoleName 
                                                group by RM.RoleName,TM.TenantName 
                                                order by TM.TenantName desc";

        public static string ValidUserTenantId = @"Select TM.TenantId from TenantMaster TM
                                                  inner join UserMaster UM
                                                  on TM.TenantId=UM.TenantId
                                                  where UM.UserName=@UserName and TM.TenantName=@TenantName";
        #endregion

        #region CompanyMaster

        public const string InsertBulkCompany = @"[dbo].[sp_insertBulkCompany]";

        public const string InsertCompanyNewMaaDurga = @"IF NOT EXISTS(Select * from [dbo].[CompanyMaster_New_Maa_Durga_Store] where CompanyName=@CompanyName) " +
                         " Begin INSERT INTO [dbo].[CompanyMaster_New_Maa_Durga_Store] (CompanyName,TenantId, CreatedDateTime,Priroty, IsActive) " +
                         "VALUES(@CompanyName,@TenantId, GETUTCDATE(),@Priroty, @IsActive) Select 1 End Else Begin Select 0 End";

        #endregion
    }
}
