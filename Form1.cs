using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAZ_Gestion_Arche
{
    public partial class Form1 : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        private DataSet XmlProjectSource = new DataSet("XMLProjectSource");
        public Form1()
        {
            InitializeComponent();

            XmlProjectSource.ReadXml(@"..\..\BDD\Project.xml");

            SE_Culture.Value = Convert.ToInt16(XmlProjectSource.Tables["NDEV"].Rows[0]["CULTURE"]);
            SE_Nourriture.Value = Convert.ToInt16(XmlProjectSource.Tables["NDEV"].Rows[0]["NOURRITURE"]);
            SE_Technologie.Value = Convert.ToInt16(XmlProjectSource.Tables["NDEV"].Rows[0]["TECHNOLOGIE"]);
            SE_Defense.Value = Convert.ToInt16(XmlProjectSource.Tables["NDEV"].Rows[0]["DEFENSE"]);

            ShowValideProjectToBuild();
        }

        private void GC_ProjectToBuild_DoubleClick(object sender, EventArgs e)
        {
            lbl_TitreProject.Text = "Projet choisi : " + XmlProjectSource.Tables["PROJECT"].Rows[((DevExpress.XtraGrid.Views.Base.ColumnView)((DevExpress.XtraGrid.GridControl)sender).FocusedView).FocusedRowHandle]["NAME"].ToString();
            Lbl_DescriptionDetail.Text = "Description : " + XmlProjectSource.Tables["PROJECT"].Rows[((DevExpress.XtraGrid.Views.Base.ColumnView)((DevExpress.XtraGrid.GridControl)sender).FocusedView).FocusedRowHandle]["DESCRIPTION"].ToString();
            lbl_CultureDetail.Text = "NDEV Culture : " + XmlProjectSource.Tables["PROJECT"].Rows[((DevExpress.XtraGrid.Views.Base.ColumnView)((DevExpress.XtraGrid.GridControl)sender).FocusedView).FocusedRowHandle]["CULTURE"].ToString();
            lbl_TechnologieDetail.Text = "NDEV Technologie : " + XmlProjectSource.Tables["PROJECT"].Rows[((DevExpress.XtraGrid.Views.Base.ColumnView)((DevExpress.XtraGrid.GridControl)sender).FocusedView).FocusedRowHandle]["TECHNOLOGIE"].ToString();
            lbl_DefenseDetail.Text = "NDEV Défense : " + XmlProjectSource.Tables["PROJECT"].Rows[((DevExpress.XtraGrid.Views.Base.ColumnView)((DevExpress.XtraGrid.GridControl)sender).FocusedView).FocusedRowHandle]["DEFENSE"].ToString();
            lbl_NourritureDetail.Text = "NDEV Nourriture : " + XmlProjectSource.Tables["PROJECT"].Rows[((DevExpress.XtraGrid.Views.Base.ColumnView)((DevExpress.XtraGrid.GridControl)sender).FocusedView).FocusedRowHandle]["NOURRITURE"].ToString();
            lbl_SkillDetail.Text = "Compétence : " + XmlProjectSource.Tables["PROJECT"].Rows[((DevExpress.XtraGrid.Views.Base.ColumnView)((DevExpress.XtraGrid.GridControl)sender).FocusedView).FocusedRowHandle]["SKILL"].ToString();
            lbl_OtherDetail.Text = "Autre pré-requis : " + XmlProjectSource.Tables["PROJECT"].Rows[((DevExpress.XtraGrid.Views.Base.ColumnView)((DevExpress.XtraGrid.GridControl)sender).FocusedView).FocusedRowHandle]["OTHER"].ToString();
            lbl_TempsTravailDetail.Text = "Temps de travail : " + XmlProjectSource.Tables["PROJECT"].Rows[((DevExpress.XtraGrid.Views.Base.ColumnView)((DevExpress.XtraGrid.GridControl)sender).FocusedView).FocusedRowHandle]["WORK"].ToString();
            lbl_SpecialDetail.Text = "Spécial : " + XmlProjectSource.Tables["PROJECT"].Rows[((DevExpress.XtraGrid.Views.Base.ColumnView)((DevExpress.XtraGrid.GridControl)sender).FocusedView).FocusedRowHandle]["SPECIAL"].ToString();
            lbl_BonusDetail.Text = "Bonus : " + XmlProjectSource.Tables["PROJECT"].Rows[((DevExpress.XtraGrid.Views.Base.ColumnView)((DevExpress.XtraGrid.GridControl)sender).FocusedView).FocusedRowHandle]["BONUS"].ToString();
            FP_DetailCard.ShowPopup();
        }

        private void FP_DetailCard_ButtonClick(object sender, DevExpress.Utils.FlyoutPanelButtonClickEventArgs e)
        {
            FP_DetailCard.HidePopup();
        }

        private void SE_Nourriture_ValueChanged(object sender, EventArgs e)
        {
           ShowValideProjectToBuild();
        }

        private void SE_Culture_ValueChanged(object sender, EventArgs e)
        {
            ShowValideProjectToBuild();
        }

        private void SE_Technologie_ValueChanged(object sender, EventArgs e)
        {
            ShowValideProjectToBuild();
        }

        private void SE_Defense_ValueChanged(object sender, EventArgs e)
        {
            ShowValideProjectToBuild();
        }

        private void ShowValideProjectToBuild()
        {
            DataSet XmlProjectSource = new DataSet("XMLProjectSource");
            XmlProjectSource.ReadXml(@"..\..\BDD\Project.xml");
            DataTable Dt_ProjectToBuild = new DataTable();
            Dt_ProjectToBuild = XmlProjectSource.Tables["PROJECT"];
            List<DataRow> DeletedRows = new List<DataRow>();
            foreach (DataRow Project in Dt_ProjectToBuild.Rows)
            {
                if (Convert.ToInt16(Project["CULTURE"]) > SE_Culture.Value)
                {
                    DeletedRows.Add(Project);
                }
                else if (Convert.ToInt16(Project["NOURRITURE"]) > SE_Nourriture.Value)
                {
                    DeletedRows.Add(Project);
                }
                else if (Convert.ToInt16(Project["TECHNOLOGIE"]) > SE_Technologie.Value)
                {
                    DeletedRows.Add(Project);
                }
                else if (Convert.ToInt16(Project["DEFENSE"]) > SE_Defense.Value)
                {
                    DeletedRows.Add(Project);
                }
            }

            foreach (DataRow ProjectToDelete in DeletedRows)
            {
                ProjectToDelete.Delete();
            }


            Dt_ProjectToBuild.AcceptChanges();
            GC_ProjectToBuild.DataSource = Dt_ProjectToBuild;
        }

    }
}
