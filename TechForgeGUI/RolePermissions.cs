using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TechForgeGUI.SubPages;

namespace TechForgeGUI.BaseForms
{
  public class RolePermissions
  {
    public string Role { get; private set; }
    public bool CanAdd { get; private set; }
    public bool CanEdit { get; private set; }
    public bool CanDelete { get; private set; }
    public bool CanView { get; private set; }
    public bool IsReadOnly { get; private set; }

    // Customer permissions
    public bool CanAddCustomers { get; private set; }
    public bool CanEditCustomers { get; private set; }
    public bool CanDeleteCustomers { get; private set; }

    // Product permissions
    public bool CanAddProducts { get; private set; }
    public bool CanEditProducts { get; private set; }
    public bool CanDeleteProducts { get; private set; }

    // Category permissions
    public bool CanAddCategories { get; private set; }
    public bool CanEditCategories { get; private set; }
    public bool CanDeleteCategories { get; private set; }

    // Manufacturer permissions
    public bool CanAddManufacturers { get; private set; }
    public bool CanEditManufacturers { get; private set; }
    public bool CanDeleteManufacturers { get; private set; }

    // Receipt permissions
    public bool CanAddReceipts { get; private set; }
    public bool CanEditReceipts { get; private set; }
    public bool CanDeleteReceipts { get; private set; }

    private static readonly Dictionary<string, RolePermissions> RoleDefaults = new Dictionary<string, RolePermissions>
    {
      { "Manager", new RolePermissions {
        Role = "Manager",
        CanAdd = true,
        CanEdit = true,
        CanDelete = true,
        CanView = true,
        IsReadOnly = false,
        CanAddCustomers = true,
        CanEditCustomers = true,
        CanDeleteCustomers = true,
        CanAddProducts = true,
        CanEditProducts = true,
        CanDeleteProducts = true,
        CanAddCategories = true,
        CanEditCategories = true,
        CanDeleteCategories = true,
        CanAddManufacturers = true,
        CanEditManufacturers = true,
        CanDeleteManufacturers = true,
        CanAddReceipts = true,
        CanEditReceipts = true,
        CanDeleteReceipts = true
      }},
      { "Cashier", new RolePermissions {
        Role = "Cashier",
        CanAdd = false,
        CanEdit = false,
        CanDelete = false,
        CanView = true,
        IsReadOnly = true,
        CanAddCustomers = true,
        CanEditCustomers = true,
        CanDeleteCustomers = true,
        CanAddProducts = false,
        CanEditProducts = false,
        CanDeleteProducts = false,
        CanAddCategories = false,
        CanEditCategories = false,
        CanDeleteCategories = false,
        CanAddManufacturers = false,
        CanEditManufacturers = false,
        CanDeleteManufacturers = false,
        CanAddReceipts = true,
        CanEditReceipts = true,
        CanDeleteReceipts = true
      }},
      { "WarehouseStaff", new RolePermissions {
        Role = "WarehouseStaff",
        CanAdd = true,
        CanEdit = true,
        CanDelete = false,
        CanView = true,
        IsReadOnly = false,
        CanAddCustomers = false,
        CanEditCustomers = false,
        CanDeleteCustomers = false,
        CanAddProducts = true,
        CanEditProducts = true,
        CanDeleteProducts = true,
        CanAddCategories = true,
        CanEditCategories = true,
        CanDeleteCategories = true,
        CanAddManufacturers = true,
        CanEditManufacturers = true,
        CanDeleteManufacturers = true,
        CanAddReceipts = false,
        CanEditReceipts = false,
        CanDeleteReceipts = false
      }}
    };
    public static RolePermissions GetPermissions(string role)
    {
      if (string.IsNullOrEmpty(role))
        return RoleDefaults["Cashier"]; // Default to Cashier permissions if role is not specified

      return RoleDefaults.ContainsKey(role) ? RoleDefaults[role] : RoleDefaults["Cashier"];
    }
  }
}