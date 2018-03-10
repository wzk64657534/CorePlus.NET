namespace CorePlus.Entity
{
    public class ViewFolderInfoEntity:BaseViewStatisticsEntity
    {
        public string AccountName { get; set; }
        public long FolderId { get; set; }
        public string FolderName { get; set; }
    }
}