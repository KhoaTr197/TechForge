using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TechForgeGUI.BaseControls.SummaryCard;

namespace TechForgeGUI.BaseControls
{
  public class SummaryCards : UserControl
  {
    protected FlowLayoutPanel container;
    public List<SummaryCard> cards;
    protected int limit;
    public SummaryCards(FlowLayoutPanel _container, int _limit = 4)
    {
      container = _container;
      limit = _limit;

      container.Resize += container_Resize;
    }

    private void container_Resize(object sender, EventArgs e)
    {
      if (container == null || container.Controls.Count == 0) return;

      int cardCount = container.Controls.Count;
      int cardWidth = (container.Width - (cardCount * 8)) / cardCount;

      foreach (Control card in container.Controls)
      {
        card.Width = cardWidth;
      }
    }
    // Method to add summary cards
    public void Add(SummaryCard[] newCards)
    {
      // Clear existing cards
      container.Controls.Clear();
      if(cards != null) cards.Clear();
      cards = newCards.ToList();

      // Limit to maximum limit cards
      int cardCount = Math.Min(newCards.Length, limit);
      if (cardCount == 0) return;

      // Calculate width percentage for each card
      int cardWidth = (container.Width - (cardCount * 8)) / cardCount;

      // Create and add cards
      for (int i = 0; i < cardCount; i++)
      {
        var card = cards[i];
        card.Width = cardWidth;
        card.Height = 80;
        card.Margin = new Padding(4);
        container.Controls.Add(card);
      }
    }
    // Method to update summary cards
    public void Update(SummaryCard[] newCards)
    {
      int count = Math.Min(newCards.Length, cards.Count);
      for (int i = 0; i < count; i++)
      {
        cards[i] = newCards[i];
      }
    }
    public void Update()
    {
      int count = cards.Count;
      for (int i = 0; i < count; i++)
      {
        cards[i].Update();
      }
    }
  }
  public class SummaryCard : UserControl
  {
    private Label lblTitle;
    private Label lblValue;
    public string Title { get; set; }
    public string Value { get; set; }
    public string Icon { get; set; }
    public SummaryCard(string title, string value, string icon, Color cardColor)
    {
      Title = title;
      Value = value;
      Icon = icon;
      BackColor = cardColor;

      InitalizeSummaryCard();
    }
    public SummaryCard(SummaryCard card)
    {
      Title = card.Title;
      Value = card.Value;
      Icon = card.Icon;
      BackColor = card.BackColor;

      InitalizeSummaryCard();
    }
    private void InitalizeSummaryCard()
    {
      // Configure panel
      this.Padding = new Padding(8);
      this.BorderStyle = BorderStyle.None;

      // Value label
      lblValue = new Label
      {
        Text = Value,
        Font = new Font("Segoe UI", 18, FontStyle.Bold),
        ForeColor = Color.White,
        AutoSize = true,
        TextAlign = ContentAlignment.MiddleLeft,
        Location = new Point(10, 10)
      };

      // Title label
      lblTitle = new Label
      {
        Text = Title,
        Font = new Font("Segoe UI", 10),
        ForeColor = Color.WhiteSmoke,
        AutoSize = true,
        TextAlign = ContentAlignment.MiddleLeft,
        Location = new Point(10, 45)
      };

      this.Controls.Add(lblValue);
      this.Controls.Add(lblTitle);
    }
    public void Update()
    {
      lblValue.Text = Value;
      lblTitle.Text = Title;
      this.BackColor = BackColor;
    }
  }
}
