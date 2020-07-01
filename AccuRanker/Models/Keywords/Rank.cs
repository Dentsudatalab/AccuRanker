namespace DentsuDataLab.AccuRanker.Models.Keywords
{
    using System;

    using LandingPages;

    using Newtonsoft.Json;

    using Utility.Fields;

    public class Rank
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("rank")]
        public int? KeywordRank { get; set; }

        [JsonProperty("share_of_voice")]
        public int? ShareOfVoice { get; set; }

        [JsonProperty("highest_ranking_page")]
        public string HighestRankingPage { get; set; }

        [JsonProperty("landing_page")]
        public LandingPage LandingPage { get; set; }

        [JsonProperty("title_description")]
        public TitleDescription TitleDescription { get; set; }

        [JsonProperty("extra_ranks")]
        public string[][] ExtraRanks { get; set; }

        [JsonProperty("is_local_result")]
        public bool? IsLocalResult { get; set; }

        [JsonProperty("is_featured_snippet")]
        public bool? IsFeaturedSnippet { get; set; }

        [JsonProperty("has_sitelinks")]
        public bool? HasSitelinks { get; set; }

        [JsonProperty("has_video")]
        public bool? HasVideo { get; set; }

        [JsonProperty("has_reviews")]
        public bool? HasReviews { get; set; }

        [JsonProperty("page_serp_features")]
        public SerpFeatures SerpFeatures { get; set; }

        [JsonProperty("above_the_fold")]
        public SerpFeatures AboveTheFold { get; set; }

        [JsonProperty("browser_position_x1")]
        public SerpFeatures BrowserPositionX1 { get; set; }

        [JsonProperty("browser_position_y1")]
        public SerpFeatures BrowserPositionY1 { get; set; }

        [JsonProperty("browser_position_x2")]
        public SerpFeatures BrowserPositionX2 { get; set; }

        [JsonProperty("browser_position_y2")]
        public SerpFeatures BrowserPositionY2 { get; set; }
    }

    public class RankFields : FieldsDescriptor
    {
        public Field Id { get; protected set; }

        public Field CreatedAt { get; protected set; }

        public Field KeywordRank { get; protected set; }

        public Field ShareOfVoice { get; protected set; }

        public Field HighestRankingPage { get; protected set; }

        public Field ExtraRanks { get; protected set; }

        public Field IsLocalResult { get; protected set; }

        public Field IsFeaturedSnippet { get; protected set; }

        public Field HasSitelinks { get; protected set; }

        public Field HasVideo { get; protected set; }

        public Field HasReviews { get; protected set; }

        public Field AboveTheFold { get; protected set; }

        public Field BrowserPositionX1 { get; protected set; }

        public Field BrowserPositionY1 { get; protected set; }

        public Field BrowserPositionX2 { get; protected set; }

        public Field BrowserPositionY2 { get; protected set; }

        public TitleDescriptionFields TitleDescription => new TitleDescriptionFields(this);

        public SerpFeaturesFields SerpFeatures => new SerpFeaturesFields(this);

        public LandingPageFields LandingPage => new LandingPageFields(this);

        public RankFields() : this(null)
        {
        }

        public RankFields(FieldsDescriptor parent) : base(parent, "ranks")
        {
            MakeFields<Rank>();
        }
    }
}