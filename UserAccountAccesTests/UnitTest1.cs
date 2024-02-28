namespace UserAccountAccesTests
{
    public class AccessRightsTests
    {
        public class User
        {
            public string Role { get; set; }
            public List<string> Roles { get; set; }
            public User(string role)
            {
                Role = role;
                Roles = new List<string> { role };
            }

            public User? CreateBrand(string name)
            {
                if (Role == "Admin" || Role == "Brand Admin")
                {
                    return new User(Role);
                }
                return null;
            }

            public bool ManageBrand()
            {
                return Role == "Admin" || Role == "Brand Admin";
            }

            public User? CreateProductList(string name)
            {
                if (Role == "Admin" || Role == "Brand Admin")
                {
                    return new User(name);
                }
                return null;
            }
            public bool ManageProductList()
            {
                return Role == "Admin" || Role == "Brand Admin";
            }

            public bool CreateUserAccount()
            {
                return Role == "Admin";
            }

            public bool ManageUserAccount()
            {
                return Role == "Admin";
            }

            public bool ManageOwnBrand()
            {
                return Role == "Brand Admin";
            }

            public bool ReadAccess()
            {
                return true;
            }

        }
        public class Brand
        {
            public string Name { get; private set; }

            public Brand(string name)
            {
                Name = name;
            }
        }

        public class ProductList
        {
            public string Name { get; private set; }

            public ProductList(string name)
            {
                Name = name;
            }
        }

        [Fact]
        public void WnenAdminCanCreateAndManageBrandsAndProductLists_ThenReturnsTrue()
        {
            //Arrange
            var admin = new User("Admin");

            //Act & Assert
            Assert.NotNull(admin.CreateBrand("Brand1"));
            Assert.NotNull(admin.CreateProductList("Product1"));
            Assert.True(admin.ManageBrand());
            Assert.True(admin.ManageProductList());
        }

        [Fact]
        public void WhenAdminCanCreateAndManageUserAccounts_ThenReturnsTrue()
        {
            //Arrange      
            var admin = new User("Admin");

            // Act & Assert
            Assert.True(admin.CreateUserAccount());
            Assert.True(admin.ManageUserAccount());
        }

        [Fact]
        public void WhenBrandAdminCanCreateAndManageBrandAndProductLists_ThenReturnstrue()
        {
            //Arrange
            var brandAdmin = new User("Brand Admin");

            // Act & Assert
            Assert.NotNull(brandAdmin.CreateBrand("Brand1"));
            Assert.True(brandAdmin.ManageBrand());
            Assert.NotNull(brandAdmin.CreateProductList("Product1"));
            Assert.True(brandAdmin.ManageProductList());

        }

        [Fact]
        public void WhenBrandAdminCanManageOwnBrand_ThenReturnsTrue()
        {
            // Arrange
            var brandAdmin = new User("Brand Admin");

            // Act & Assert
            Assert.True(brandAdmin.ManageOwnBrand());
        }

        [Fact]
        public void WnehUserRolesHasReadonlyPermissions_ThenReturnsFalseForAdminActionsAndReturnsTrueForReadyOnly()
        {
            //Arrange
            var basicUser = new User("User");

            // Act & Assert
            Assert.Null(basicUser.CreateBrand("Brand1"));
            Assert.False(basicUser.ManageBrand());
            Assert.Null(basicUser.CreateProductList("Product1"));
            Assert.False(basicUser.ManageProductList());
            Assert.False(basicUser.CreateUserAccount());
            Assert.False(basicUser.ManageUserAccount());
            Assert.True(basicUser.ReadAccess());
        }

    }
}