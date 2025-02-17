using System;
using System.Collections.Generic;
using System.Linq;

namespace Clover.Gestion
{
    public class ViewSettings
    {
        public int MaxFilters { get; }
        public int MaxSortLevels { get; }
        public DataField[] Fields { get; }
        public static Dictionary<int, string> GenericConditions = new Dictionary<int, string>
        {
            { 1, " {0} {1} = '{2}'" },
            { 2, " {0} {1} <> '{2}'" },
            { 7, " {0} {1} > '{2}'" },
            { 8, " {0} {1} >= '{2}'" },
            { 9, " {0} {1} < '{2}'" },
            { 10, " {0} {1} <= '{2}'" }
        };
        public static Dictionary<int, string> StringConditions = new Dictionary<int, string>
        {
            { 1, " {0} {1} LIKE '{2}'" },
            { 2, " {0} {1} NOT LIKE '{2}'" },
            { 3, " {0} {1} LIKE '{2}%'" },
            { 4, " {0} {1} LIKE '%{2}'" },
            { 5, " {0} {1} LIKE '%{2}%'" },
            { 6, " {0} {1} NOT LIKE '%{2}%'" }
         };

        public List<SortLevel> SortLevels { get; set; }
        public List<Filter> Filters { get; set; }
        public bool AllConditionsMustBeTrue { get; set; }

        public ViewSettings(int MaxFilters, int MaxSortLevels, DataField[] Fields)
        {
            this.MaxFilters = MaxFilters;
            this.MaxSortLevels = MaxSortLevels;
            this.Fields = Fields;
            SortLevels = new List<SortLevel>();
            Filters = new List<Filter>();
            AllConditionsMustBeTrue = true;
        }

        public void AddSortLevel(string DataPropertyName, SortDirection Direction)
        {
            SortLevels.Add(new SortLevel(Fields.Single(X => X.Name == DataPropertyName), Direction));
        }
        public bool TryAddSortLevel(string DataPropertyName, SortDirection Direction)
        {
            var lookup = SortLevels.Where(X => X.Field.Name == DataPropertyName);
            if (lookup.Count() != 0)
            {
                lookup.Single().Direction = Direction;
                return true;
            }
            else if (SortLevels.Count < MaxSortLevels)
            {
                SortLevels.Add(new SortLevel(Fields.Single(X => X.Name == DataPropertyName), Direction));
                return true;
            }
            else
            {
                return false;
            }
        }
        public void RemoveSortLevel(string DataPropertyName)
        {
            var lookup = SortLevels.Where(X => X.Field.Name == DataPropertyName);
            if (lookup.Count() != 0)
            {
                SortLevels.Remove(lookup.Single());
            }
        }

        public override string ToString()
        {
            string mainClause = string.Empty;
            string joinKeyword = AllConditionsMustBeTrue ? "AND" : "OR";
            var filtersWithOutAliases = Filters.Where(X => !X.Field.IsAlias);
            if (filtersWithOutAliases.Count() != 0)
            {
                string clause = string.Empty;
                foreach (var filter in filtersWithOutAliases)
                {
                    switch (filter.Field.FieldType)
                    {
                        case DataFieldTypes.String:
                            clause += string.Format(StringConditions[filter.ConditionID], joinKeyword, filter.Field.Name, filter.Value);
                            break;
                        case DataFieldTypes.DateTime:
                            clause += string.Format(GenericConditions[filter.ConditionID], joinKeyword, filter.Field.Name, DateTime.Parse(filter.Value).ToString("yyyy-MM-dd"));
                            break;
                        case DataFieldTypes.Integer:
                            clause += string.Format(GenericConditions[filter.ConditionID], joinKeyword, filter.Field.Name, filter.Value);
                            break;
                    }
                }
                mainClause += " WHERE " + clause.Substring(joinKeyword.Length + 2);
            }
            var filtersWithAliases = Filters.Where(X => X.Field.IsAlias);
            if (filtersWithAliases.Count() != 0)
            {
                string clause = string.Empty;
                foreach (var filter in filtersWithAliases)
                {
                    switch (filter.Field.FieldType)
                    {
                        case DataFieldTypes.String:
                            clause += string.Format(StringConditions[filter.ConditionID], joinKeyword, filter.Field.Name, filter.Value);
                            break;
                        case DataFieldTypes.DateTime:
                            clause += string.Format(GenericConditions[filter.ConditionID], joinKeyword, filter.Field.Name, DateTime.Parse(filter.Value).ToString("yyyy-MM-dd"));
                            break;
                        case DataFieldTypes.Integer:
                            clause += string.Format(GenericConditions[filter.ConditionID], joinKeyword, filter.Field.Name, filter.Value);
                            break;
                    }
                }
                mainClause += " HAVING " + clause.Substring(joinKeyword.Length + 2);
            }
            if (SortLevels.Count != 0)
            {
                string clause = string.Empty;
                foreach (var level in SortLevels)
                {
                    clause += ", " + level.Field.Name + " " + level.Direction.ToString();
                }
                mainClause += " ORDER BY " + clause.Substring(2);
            }
            return mainClause;
        }
    }

    public class DataField
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public DataFieldTypes FieldType { get; set; }
        public bool IsAlias { get; set; }

        public DataField(string Name, string Caption, DataFieldTypes FieldType, bool IsAlias)
        {
            this.Name = Name;
            this.Caption = Caption;
            this.FieldType = FieldType;
            this.IsAlias = IsAlias;
        }
        public DataField(string Name, string Caption, DataFieldTypes FieldType)
        {
            this.Name = Name;
            this.Caption = Caption;
            this.FieldType = FieldType;
            this.IsAlias = false;
        }
    }
    public enum DataFieldTypes { DateTime, Integer, String }

    public class Filter
    {
        public DataField Field { get; set; }
        public int ConditionID { get; set; }
        public string Value { get; set; }

        public Filter(DataField Field, int ConditionID, string Value)
        {
            this.Field = Field;
            this.ConditionID = ConditionID;
            this.Value = Value;
        }
    }

    public class SortLevel
    {
        public DataField Field { get; set; }
        public SortDirection Direction { get; set; }

        public SortLevel(DataField Field, SortDirection Direction)
        {
            this.Field = Field;
            this.Direction = Direction;
        }
    }
    public enum SortDirection { ASC = 1, DESC = 2 }
    
}