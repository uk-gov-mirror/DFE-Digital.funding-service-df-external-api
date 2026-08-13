using System;
using System.Collections.Generic;
using DigitalForms.BL.Models;
using DigitalForms.BL.Models.Generated;
using Microsoft.EntityFrameworkCore;

namespace DigitalForms.BL.Data;

public partial class s255d01dbDfSharedContext : DbContext
{
    public s255d01dbDfSharedContext()
    {
    }

    public s255d01dbDfSharedContext(DbContextOptions<s255d01dbDfSharedContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calculation> Calculations { get; set; }

    public virtual DbSet<CalculationComponentDetail> CalculationComponentDetails { get; set; }
    
    public virtual DbSet<Component> Components { get; set; }

    public virtual DbSet<ComponentAdditionalSetting> ComponentAdditionalSettings { get; set; }

    public virtual DbSet<ComponentDatasetSchemaDetail> ComponentDatasetSchemaDetails { get; set; }

    public virtual DbSet<ComponentDatasetSchemaPropertyDetail> ComponentDatasetSchemaPropertyDetails { get; set; }

    public virtual DbSet<Condition> Conditions { get; set; }

    public virtual DbSet<ConditionDetail> ConditionDetails { get; set; }

    public virtual DbSet<DatasetDataDetail> DatasetDataDetails { get; set; }

    public virtual DbSet<DesignDataSet> DesignDataSets { get; set; }

    public virtual DbSet<DesignDataSetDetail> DesignDataSetDetails { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Fee> Fees { get; set; }

    public virtual DbSet<Form> Forms { get; set; }

    public virtual DbSet<FormStatus> FormStatuses { get; set; }

    public virtual DbSet<Formsjson> Formsjsons { get; set; }

    public virtual DbSet<List> Lists { get; set; }

    public virtual DbSet<ListItem> ListItems { get; set; }

    public virtual DbSet<MetaData> MetaData { get; set; }

    public virtual DbSet<OrganisationDetail> OrganisationDetails { get; set; }

    public virtual DbSet<Output> Outputs { get; set; }

    public virtual DbSet<OutputDetail> OutputDetails { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<PageChildSetting> PageChildSettings { get; set; }

    public virtual DbSet<ProviderMapping> ProviderMappings { get; set; }

    public virtual DbSet<Providersmappingjson> Providersmappingjsons { get; set; }

    public virtual DbSet<Response> Responses { get; set; }

    public virtual DbSet<ResponseQuestion> ResponseQuestions { get; set; }

    public virtual DbSet<ResponseQuestionDatum> ResponseQuestionData { get; set; }

    public virtual DbSet<Responsesjson> Responsesjsons { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<TabChildDetail> TabChildDetails { get; set; }

    public virtual DbSet<TabDetail> TabDetails { get; set; }

    public virtual DbSet<TblId> TblIds { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    public virtual DbSet<UserOrganisationDetail> UserOrganisationDetails { get; set; }

    public virtual DbSet<CalculationAdditionalSetting> CalculationAdditionalSettings { get; set; }

    // Parent child configurations
    public virtual DbSet<ParentChild> ParentChilds { get; set; }
    public virtual DbSet<ParentChildConfig> ParentChildconfigs { get; set; }
    public virtual DbSet<ChildConfig> ChildConfigs { get; set; }
    public virtual DbSet<DependentForm> DependentForms { get; set; }
    public virtual DbSet<DraftResponse> DraftResponse { get; set; }
    public virtual DbSet<SubmissionFormLog> SubmissionFormLog { get; set; }
    public virtual DbSet<RepeatableFormsData> RepeatableFormsData { get; set; }
    public virtual DbSet<RepeatableSectionsData> RepeatableSectionsData { get; set; }
    public virtual DbSet<RepeatableFormsMapper> RepeatableFormsMapper { get; set; }

    public virtual DbSet<DCData> DCData { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<DCData>(entity =>
        {
            entity.HasKey(e => e.DCDID).HasName("PK__DCData__6E1617B4AAF46C7B ");

        });
        modelBuilder.Entity<DraftResponse>(entity =>
        {

            entity.HasKey(e => e.DRID).HasName("PK__DraftRes__23BEF8810D0F71ED");

            entity.HasOne(d => d.UserOrganisationDetails)
                .WithMany(p => p.DraftResponses)
                .HasForeignKey(d => d.UserOrgID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DraftResponse_UserOrgID");

            entity.HasOne(d => d.Outputs)
                .WithMany(p => p.DraftResponses)
                .HasForeignKey(d => d.OUID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DraftResponse_OUID");


            entity.HasOne(d => d.Responses)
                .WithMany(p => p.DraftResponses)
                .HasForeignKey(d => d.RSPID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DraftResponse_RSPID");

            //    entity.HasMany(d => d.Outputs)
            //         .WithMany(p => p.DraftResponses)
            //          .UsingEntity<Dictionary<string, object>>(
            //    "DraftResponseOutput",
            //    j => j.HasOne<Output>()
            //          .WithMany()
            //          .HasForeignKey("OUID")
            //          .HasConstraintName("FK_DraftResponse_OUID"),
            //    j => j.HasOne<DraftResponse>()
            //          .WithMany()
            //          .HasForeignKey("DRID")
            //          .HasConstraintName("FK_DraftResponse_DRID")
            //);

            //entity.HasOne(d => d.Responses)
            //    .WithMany(p => p.DraftResponses)
            //    .HasForeignKey(d => d.RSPID)
            //    .OnDelete(DeleteBehavior.Cascade)
            //    .HasConstraintName("FK_DraftResponse_RSPID");
        });

        modelBuilder.Entity<RepeatableSectionsData>(entity =>
        {
            entity.HasKey(e => e.RFSID).HasName("PK__Repeatab__B53B89DD947B8777 ");

        });
        modelBuilder.Entity<RepeatableFormsMapper>(entity =>
        {
            entity.HasKey(e => e.RFMID).HasName("PK__Repeatab__B125CC2D823B3718"); 
            entity.HasKey(e => e.RFID).HasName("FK_RepeatableFormsMapper_RFID");
            entity.HasKey(e => e.RFSID).HasName("FK_RepeatableFormsMapper_RFSID");

        });
        modelBuilder.Entity<RepeatableFormsData>(entity =>
        {
            entity.HasKey(e => e.RFID).HasName("PK__Repeatab__4508DC094D60C06B");
             
        });

        modelBuilder.Entity<SubmissionFormLog>(entity =>
        {
            entity.HasKey(e => e.SFid).HasName("PK__Submissi__F4AD989E3578264B");
        });

        modelBuilder.Entity<Calculation>(entity =>
        {
            entity.HasKey(e => e.Calcid).HasName("PK__Calculat__10DEE2B57224A081");

            entity.Property(e => e.HideResult).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.FidNavigation).WithMany(p => p.Calculations)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Calculations.FID");
        });

        modelBuilder.Entity<CalculationComponentDetail>(entity =>
        {
            entity.HasKey(e => e.Calcoid).HasName("PK__Calculat__10DEE2B51D1F4E39");

            entity.HasOne(d => d.Calc).WithMany(p => p.CalculationComponentDetails)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CalculationChildDetails_CALCID");

            entity.HasOne(d => d.Cmp).WithMany(p => p.CalculationComponentDetails)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CalculationChildDetails_CMPID");
        });

        modelBuilder.Entity<CalculationAdditionalSetting>(entity =>
        {
            entity.HasKey(e => e.Calasid).HasName("PK__Calculat__18E412599FBF4797");

            entity.HasOne(d => d.Calc).WithMany(p => p.CalculationAdditionalSettings)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_CalculationAdditionalSettings.CALCID");

        });

        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.Cmpid).HasName("PK__Componen__0461AC5AEAAB918D");

            entity.Property(e => e.AdditionalSettings).HasDefaultValueSql("((0))");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Dds).WithMany(p => p.Components).HasConstraintName("FK_Component.DDSID");

            entity.HasOne(d => d.Doc).WithMany(p => p.Components).HasConstraintName("FK_Components_DOCID");

            entity.HasOne(d => d.Lst).WithMany(p => p.Components).HasConstraintName("FK_Component.LSTID");

            entity.HasOne(d => d.Pg).WithMany(p => p.Components)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Component.PGID");
        });
        // parent and child configs start
        modelBuilder.Entity<ParentChild>(entity =>
        {
            entity.HasKey(e => e.Pcid).HasName("PK__ParentCh__580221FF8B7EA582");

            entity.HasOne(d => d.FidNavigation).WithOne(p => p.ParentChild)
                .OnDelete(DeleteBehavior.Cascade).HasForeignKey<ParentChild>(e => e.Fid)
                .HasConstraintName("FK_ParentChild__FID");
        });

        modelBuilder.Entity<ParentChildConfig>(entity =>
        {
            entity.HasKey(e => e.Pccid).HasName("PK__ParentCh__7ED10C2CD4D96B64");

            entity.HasOne(d => d.PCCParentChild).WithOne(p => p.ParentChildConfig)
            .OnDelete(DeleteBehavior.Cascade).HasForeignKey<ParentChildConfig>(e => e.Pcid)
                .HasConstraintName("FK_ParentChildConfig__PCID");
        });

        modelBuilder.Entity<ChildConfig>(entity =>
        {
            entity.HasKey(e => e.Ccid).HasName("PK__ChildCon__A9561A42683DCF56");

            entity.HasOne(d => d.CCParentChildConfig).WithMany(p => p.ChildConfigs)
            .OnDelete(DeleteBehavior.Cascade).HasForeignKey(e => new { e.Pccid })
                .HasConstraintName("FK_ChildConfigs__PCCID");
        });

        modelBuilder.Entity<DependentForm>(entity =>
        {
            entity.HasKey(e => e.Dfid).HasName("PK__Dependen__2A9DE84FF2C68EE1");

            entity.HasOne(d => d.DFChildConfigs).WithMany(p => p.DependentForms)
               .OnDelete(DeleteBehavior.Cascade).HasForeignKey(e => new { e.Ccid })
                .HasConstraintName("FK_DependentForms__CCID");
        });

        // parent and child configs end

        modelBuilder.Entity<ComponentAdditionalSetting>(entity =>
        {
            entity.HasKey(e => e.Cadstid).HasName("PK__Addition__B0CF7AF245624530");

            entity.HasOne(d => d.Cmp).WithMany(p => p.ComponentAdditionalSettings)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_AdditionalSettings.CMPID");
        });

        modelBuilder.Entity<ComponentDatasetSchemaDetail>(entity =>
        {
            entity.HasKey(e => e.Cdsdid).HasName("PK__DatasetS__29D3C178E258C48D");

            entity.HasOne(d => d.Cmp).WithMany(p => p.ComponentDatasetSchemaDetails)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DatasetSchemaDetails.CMPID");
        });

        modelBuilder.Entity<ComponentDatasetSchemaPropertyDetail>(entity =>
        {
            entity.HasKey(e => e.Cdspdid).HasName("PK__ChildDat__D2366237F598F057");

            entity.HasOne(d => d.Cdsd).WithMany(p => p.ComponentDatasetSchemaPropertyDetails)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ChildDatasetSchemaDetails.DSID");
        });

        modelBuilder.Entity<Condition>(entity =>
        {
            entity.HasKey(e => e.Cnid).HasName("PK__Conditio__AA570FD4357D8D12");

            entity.HasOne(d => d.FidNavigation).WithMany(p => p.Conditions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Condition.FID");
        });

        modelBuilder.Entity<ConditionDetail>(entity =>
        {
            entity.HasKey(e => e.Cndtlid).HasName("PK__Conditio__EC723B4810BF6ACF");

            entity.HasOne(d => d.Cn).WithMany(p => p.ConditionDetails)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ConditionDetails.CNID");
        });

        modelBuilder.Entity<DatasetDataDetail>(entity =>
        {
            entity.HasKey(e => e.Ddid).HasName("PK__DatasetD__2612A72A327AC34B");

            entity.HasOne(d => d.Cdsd).WithMany(p => p.DatasetDataDetails)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DatasetDataDetails.DSID");
        });

        modelBuilder.Entity<DesignDataSet>(entity =>
        {
            entity.HasKey(e => e.Ddsid).HasName("PK__DesignDa__469DEB1D593C79EB");

            entity.HasOne(d => d.Doc).WithMany(p => p.DesignDataSets)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_DesignDataSet.DOCID");

            entity.HasOne(d => d.FidNavigation).WithMany(p => p.DesignDataSets)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DesignDataSet.FID");
        });

        modelBuilder.Entity<DesignDataSetDetail>(entity =>
        {
            entity.HasKey(e => e.Ddsdtlid).HasName("PK__DesignDa__B017E4D03805AFCB");

            entity.HasOne(d => d.Dds).WithMany(p => p.DesignDataSetDetails)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_DesignDataSetDetails.DDSID");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Docid).HasName("PK__Document__CEBF0D81C58A7707");

            entity.HasOne(d => d.FidNavigation).WithMany(p => p.Documents)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Document.FID");
        });

        modelBuilder.Entity<Fee>(entity =>
        {
            entity.HasKey(e => e.Feid).HasName("PK__Fees__91F726CDC9CAC9F0");

            entity.HasOne(d => d.FidNavigation).WithMany(p => p.Fees).OnDelete(DeleteBehavior.Cascade).HasConstraintName("FK_Fees.FID");
        });

        modelBuilder.Entity<Form>(entity =>
        {
            entity.HasKey(e => e.Fid).HasName("PK__Forms__C1BEA5A20F208FB0");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.LastUpdatedOn).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.FormCreatedByUsers).HasConstraintName("FK_Forms_CreateUID");

            entity.HasOne(d => d.Fs).WithMany(p => p.Forms)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_FormStatus.FSID");

            entity.HasOne(d => d.LastUpdatedByUser).WithMany(p => p.FormLastUpdatedByUsers).HasConstraintName("FK_Forms_ModifyUID");
        });

        modelBuilder.Entity<FormStatus>(entity =>
        {
            entity.HasKey(e => e.Fsid).HasName("PK__FormStat__9C4B073666EFF090");
        });

        modelBuilder.Entity<Formsjson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__formsjso__3213E83FD99569AE");
        });

        modelBuilder.Entity<List>(entity =>
        {
            entity.HasKey(e => e.Lstid).HasName("PK__List__14F65955D8769B9E");

            entity.HasOne(d => d.FidNavigation).WithMany(p => p.Lists)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_List.FID");
        });

        modelBuilder.Entity<ListItem>(entity =>
        {
            entity.HasKey(e => e.Lstitemid).HasName("PK__ListItem__D4FBA39357D952DF");

            entity.HasOne(d => d.Lst).WithMany(p => p.ListItems)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ListItems.LSTID");
        });

        modelBuilder.Entity<MetaData>(entity =>
        {
            entity.HasKey(e => e.Mtdtid).HasName("PK__MetaData__2A8F51D2A488B2FE");

            entity.HasOne(d => d.FidNavigation).WithOne(p => p.MetaData).OnDelete(DeleteBehavior.Cascade).HasConstraintName("FK_MetaData.FID");
        });

        modelBuilder.Entity<OrganisationDetail>(entity =>
        {
            entity.HasKey(e => e.Orgid).HasName("PK__Organisa__1EDD7BD661B3D408");

         });
        modelBuilder.Entity<UserOrganisationDetail>(entity =>
        {
            entity.HasKey(e => e.UserOrgID).HasName("PK__UserOrga__797DCBF13719BB86");
            entity.HasKey(e => e.UserOrgID).HasName("FK_UserOrganisationDetails_ORGID");
            entity.HasKey(e => e.UserOrgID).HasName("FK_UserOrganisationDetails_Userid");
        });
        modelBuilder.Entity<Output>(entity =>
        {
            entity.HasKey(e => e.Ouid).HasName("PK__Outputs__A86120A2FCCC4557");

            entity.HasOne(d => d.FidNavigation).WithMany(p => p.Outputs)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Outputs.FID");
        });

        modelBuilder.Entity<OutputDetail>(entity =>
        {
            entity.HasKey(e => e.Oudtlid).HasName("PK__OutputDe__E048271F9E4E9C1F");

            entity.HasOne(d => d.Ou).WithMany(p => p.OutputDetails)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_OutputDetails.OUID");
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.HasKey(e => e.Pgid).HasName("PK__Pages__5902017CD61BCE90");

            entity.HasOne(d => d.FidNavigation).WithMany(p => p.Pages)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Pages.FID");
        });

        modelBuilder.Entity<PageChildSetting>(entity =>
        {
            entity.HasKey(e => e.Pgchstid).HasName("PK__PageChil__CE6700FB7CA6FB8A");

            entity.HasOne(d => d.Pg).WithMany(p => p.PageChildSettings)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PageChildSettings.PGID");
        });

        modelBuilder.Entity<ProviderMapping>(entity =>
        {
            entity.HasKey(e => e.Pmid).HasName("PK__Provider__5C86FF66F18826B7");
            //entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<UKPRNProvidersData>(entity =>
        {
            entity.HasKey(e => e.Ukpdid).HasName("PK__UKPRNPro__4BA33E28FD494B12");

            entity.HasOne(d => d.PmUKPRN).WithMany(p => p.UKPRNProvidersData)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__UKPRNProvi__PMID__25FB978D");
        });

        modelBuilder.Entity<URNProvidersData>(entity =>
        {
            entity.HasKey(e => e.Urpdid).HasName("PK__URNProvi__906B48367940A498");

            entity.HasOne(d => d.PmURN).WithMany(p => p.URNProvidersData)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__URNProvide__PMID__28D80438");
        });

        modelBuilder.Entity<AdminCodeProvidersData>(entity =>
        {
            entity.HasKey(e => e.Acpdid).HasName("PK__AdminCod__2BEC24C4B1268DE9");

            entity.HasOne(d => d.PmAC).WithMany(p => p.AdminCodeProvidersData)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__AdminCodeP__PMID__2BB470E3");
        });

        modelBuilder.Entity<Providersmappingjson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__provider__3213E83F512DC7C9");
        });

        modelBuilder.Entity<Response>(entity =>
        {
            entity.HasKey(e => e.Rspid).HasName("PK__Response__9956C373C570B41F");

            entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Mtdt).WithMany(p => p.Responses)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Responses_MetaData");

            entity.HasOne(d => d.User).WithMany(p => p.Responses)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Responses_Userid");

            entity.HasOne(d => d.UserOrganisationDetails).WithMany(p => p.Responses)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Responses_UserOrgID");
        });

        modelBuilder.Entity<ResponseQuestion>(entity =>
        {
            entity.HasKey(e => e.RspQstId).HasName("PK__Response__AA2B53B8570CFD88");

            entity.HasOne(d => d.Rsp).WithMany(p => p.ResponseQuestions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ResponseQuestions_Responses");
        });

        modelBuilder.Entity<ResponseQuestionDatum>(entity =>
        {
            entity.HasKey(e => e.RspQstDataId).HasName("PK__Response__04481870D66DC8B6");

            entity.HasOne(d => d.RspQst).WithMany(p => p.ResponseQuestionData)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ResponseQuestionData_ResponseQuestions");
        });

        modelBuilder.Entity<Responsesjson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__response__3213E83F52E39EC2");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.Scid).HasName("PK__Sections__F7FE93AC54A8462A");

            entity.HasOne(d => d.FidNavigation).WithMany(p => p.Sections)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Sections.FID");
        });

        modelBuilder.Entity<TabChildDetail>(entity =>
        {
            entity.HasKey(e => e.Tabcid).HasName("PK__ChildTab__372B2E0B61B182AE");

            entity.HasOne(d => d.Cadst).WithMany(p => p.TabChildDetails).HasConstraintName("FK_ChildTabsDetails.ADSTID");

            entity.HasOne(d => d.Tab).WithMany(p => p.TabChildDetails)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ChildTabDetails_TABID");
        });

        modelBuilder.Entity<TabDetail>(entity =>
        {
            entity.HasKey(e => e.Tabid).HasName("PK_TabDetails_TABID");

            entity.HasOne(d => d.FidNavigation).WithMany(p => p.TabDetails)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_TabDetails_FID");
        });

        modelBuilder.Entity<TblId>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Ids__3214EC07E251B901");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("PK__UserDeta__C5B19602BC93CE44");
            entity.Property(e => e.Status).HasDefaultValueSql("1");
        });
        modelBuilder.Entity<Calculation>().Property(e => e.Calcid).ValueGeneratedOnAdd();
        modelBuilder.Entity<CalculationComponentDetail>().Property(e => e.Calcoid).ValueGeneratedOnAdd();
        modelBuilder.Entity<Component>().Property(e => e.Cmpid).ValueGeneratedOnAdd();
        modelBuilder.Entity<ComponentAdditionalSetting>().Property(e => e.Cadstid).ValueGeneratedOnAdd();
        modelBuilder.Entity<ComponentDatasetSchemaDetail>().Property(e => e.Cdsdid).ValueGeneratedOnAdd();
        modelBuilder.Entity<ComponentDatasetSchemaPropertyDetail>().Property(e => e.Cdspdid).ValueGeneratedOnAdd();
        modelBuilder.Entity<Condition>().Property(e => e.Cnid).ValueGeneratedOnAdd();
        modelBuilder.Entity<ConditionDetail>().Property(e => e.Cndtlid).ValueGeneratedOnAdd();
        modelBuilder.Entity<DatasetDataDetail>().Property(e => e.Ddid).ValueGeneratedOnAdd();
        modelBuilder.Entity<DesignDataSet>().Property(e => e.Ddsid).ValueGeneratedOnAdd();
        modelBuilder.Entity<DesignDataSetDetail>().Property(e => e.Ddsdtlid).ValueGeneratedOnAdd();
        modelBuilder.Entity<Document>().Property(e => e.Docid).ValueGeneratedOnAdd();
        modelBuilder.Entity<Fee>().Property(e => e.Feid).ValueGeneratedOnAdd();
        modelBuilder.Entity<Form>().Property(e => e.Fid).ValueGeneratedOnAdd();
        modelBuilder.Entity<FormStatus>().Property(e => e.Fsid).ValueGeneratedOnAdd();
        modelBuilder.Entity<List>().Property(e => e.Lstid).ValueGeneratedOnAdd();
        modelBuilder.Entity<ListItem>().Property(e => e.Lstitemid).ValueGeneratedOnAdd();
        modelBuilder.Entity<MetaData>().Property(e => e.Mtdtid).ValueGeneratedOnAdd();
        modelBuilder.Entity<OrganisationDetail>().Property(e => e.Orgid).ValueGeneratedOnAdd();
        modelBuilder.Entity<Output>().Property(e => e.Ouid).ValueGeneratedOnAdd();
        modelBuilder.Entity<OutputDetail>().Property(e => e.Oudtlid).ValueGeneratedOnAdd();
        modelBuilder.Entity<Page>().Property(e => e.Pgid).ValueGeneratedOnAdd();
        modelBuilder.Entity<PageChildSetting>().Property(e => e.Pgchstid).ValueGeneratedOnAdd();
        modelBuilder.Entity<ProviderMapping>().Property(e => e.Pmid).ValueGeneratedOnAdd();
        modelBuilder.Entity<Response>().Property(e => e.Rspid).ValueGeneratedOnAdd();
        modelBuilder.Entity<ResponseQuestion>().Property(e => e.RspQstId).ValueGeneratedOnAdd();
        modelBuilder.Entity<ResponseQuestionDatum>().Property(e => e.RspQstDataId).ValueGeneratedOnAdd();
        modelBuilder.Entity<Section>().Property(e => e.Scid).ValueGeneratedOnAdd();
        modelBuilder.Entity<TabChildDetail>().Property(e => e.Tabcid).ValueGeneratedOnAdd();
        modelBuilder.Entity<TabDetail>().Property(e => e.Tabid).ValueGeneratedOnAdd();
        modelBuilder.Entity<UserDetail>().Property(e => e.Uid).ValueGeneratedOnAdd();
        modelBuilder.Entity<TblId>().Property(e => e.Id).ValueGeneratedOnAdd();
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
