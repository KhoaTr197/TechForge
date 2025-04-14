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

    // Page-specific permissions
    public bool CanManageCustomers { get; private set; }
    public bool CanManageProducts { get; private set; }
    public bool CanManageCategories { get; private set; }
    public bool CanManageManufacturers { get; private set; }
    public bool CanManageReceipts { get; private set; }

    private static readonly Dictionary<string, RolePermissions> RoleDefaults = new Dictionary<string, RolePermissions>
    {
      { "Admin", new RolePermissions { 
        Role = "Admin", 
        CanAdd = true, 
        CanEdit = true, 
        CanDelete = true, 
        CanView = true, 
        IsReadOnly = false,
        CanManageCustomers = true,
        CanManageProducts = true,
        CanManageCategories = true,
        CanManageManufacturers = true,
        CanManageReceipts = true
      }},
      { "Manager", new RolePermissions { 
        Role = "Manager", 
        CanAdd = true, 
        CanEdit = true, 
        CanDelete = true, 
        CanView = true, 
        IsReadOnly = false,
        CanManageCustomers = true,
        CanManageProducts = true,
        CanManageCategories = true,
        CanManageManufacturers = true,
        CanManageReceipts = true
      }},
      { "Cashier", new RolePermissions { 
        Role = "Cashier", 
        CanAdd = false, 
        CanEdit = false, 
        CanDelete = false, 
        CanView = true, 
        IsReadOnly = true,
        CanManageCustomers = true,
        CanManageProducts = false,
        CanManageCategories = false,
        CanManageManufacturers = false,
        CanManageReceipts = true
      }},
      { "WarehouseStaff", new RolePermissions { 
        Role = "WarehouseStaff", 
        CanAdd = true, 
        CanEdit = true, 
        CanDelete = false, 
        CanView = true, 
        IsReadOnly = false,
        CanManageCustomers = false,
        CanManageProducts = true,
        CanManageCategories = true,
        CanManageManufacturers = true,
        CanManageReceipts = false
      }}
    };

    public static RolePermissions GetPermissions(string role)
    {
      if (string.IsNullOrEmpty(role))
        return RoleDefaults["Cashier"]; // Default to Staff permissions if role is not specified

      return RoleDefaults.ContainsKey(role) ? RoleDefaults[role] : RoleDefaults["Cashier"];
    }

    public void ApplyToForm(DetailFormGUI form)
    {
      if (form == null) return;

      // Apply general permissions
      form.btnAdd.Visible = CanAdd;
      form.btnAdd.Enabled = CanAdd;
      form.btnEdit.Visible = CanEdit;
      form.btnEdit.Enabled = CanEdit;
      form.btnDelete.Visible = CanDelete;
      form.btnDelete.Enabled = CanDelete;

      // Apply page-specific permissions based on form type
      if (form is CustomerDetailFormGUI)
      {
        form.btnAdd.Visible = CanManageCustomers;
        form.btnAdd.Enabled = CanManageCustomers;
        form.btnEdit.Visible = CanManageCustomers;
        form.btnEdit.Enabled = CanManageCustomers;
        form.btnDelete.Visible = CanManageCustomers;
        form.btnDelete.Enabled = CanManageCustomers;
      }
      else if (form is ProductDetailFormGUI)
      {
        form.btnAdd.Visible = CanManageProducts;
        form.btnAdd.Enabled = CanManageProducts;
        form.btnEdit.Visible = CanManageProducts;
        form.btnEdit.Enabled = CanManageProducts;
        form.btnDelete.Visible = CanManageProducts;
        form.btnDelete.Enabled = CanManageProducts;
      }
      else if (form is CategoryDetailFormGUI)
      {
        form.btnAdd.Visible = CanManageCategories;
        form.btnAdd.Enabled = CanManageCategories;
        form.btnEdit.Visible = CanManageCategories;
        form.btnEdit.Enabled = CanManageCategories;
        form.btnDelete.Visible = CanManageCategories;
        form.btnDelete.Enabled = CanManageCategories;
      }
      else if (form is ReceiptDetailFormGUI)
      {
        form.btnAdd.Visible = false;
        form.btnAdd.Enabled = false;
        form.btnEdit.Visible = CanManageReceipts;
        form.btnEdit.Enabled = CanManageReceipts;
        form.btnDelete.Visible = CanManageReceipts;
        form.btnDelete.Enabled = CanManageReceipts;
      }

      if (form.Controls["flpInfoPanel"] is FlowLayoutPanel flpInfoPanel)
      {
        foreach (Control panel in flpInfoPanel.Controls)
        {
          if (panel is Panel infoPanel)
          {
            foreach (Control control in infoPanel.Controls)
            {
              if (control is Label)
              {
                continue;
              }
              else if (control is TextBox textBox)
              {
                textBox.ReadOnly = IsReadOnly;
                textBox.Enabled = true;
              }
              else
              {
                control.Enabled = !IsReadOnly;
              }
            }
          }
        }
      }
    }

    public void ApplyToManagePage(ManagePage page)
    {
      if (page == null) return;

      // Apply general permissions
      page.btnAdd.Visible = CanAdd;
      page.btnAdd.Enabled = CanAdd;

      // Apply page-specific permissions based on page type
      if (page is CustomerManagePageGUI)
      {
        page.btnAdd.Visible = CanManageCustomers;
        page.btnAdd.Enabled = CanManageCustomers;
      }
      else if (page is ProductManagePageGUI)
      {
        page.btnAdd.Visible = CanManageProducts;
        page.btnAdd.Enabled = CanManageProducts;
      }
      else if (page is CategoryManagePageGUI)
      {
        page.btnAdd.Visible = CanManageCategories;
        page.btnAdd.Enabled = CanManageCategories;
      }
      else if (page is ManufacturerManagePageGUI)
      {
        page.btnAdd.Visible = CanManageManufacturers;
        page.btnAdd.Enabled = CanManageManufacturers;
      }
    }
  }
}