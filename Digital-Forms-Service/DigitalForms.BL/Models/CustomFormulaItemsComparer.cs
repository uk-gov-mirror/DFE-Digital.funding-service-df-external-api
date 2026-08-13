using DigitalForms.BL.Models;
using DigitalForms.BL.Serialized.Models;
using ObjectsComparer;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalForms.BL.Models
{
    public class CustomComparer<T> : AbstractComparer<IList<T>> where T : class
    {
        public CustomComparer(ComparisonSettings settings, BaseComparer parentComparer, IComparersFactory factory) : base(settings, parentComparer, factory)
        {
        }

        public override IEnumerable<Difference> CalculateDifferences(IList<T> obj1, IList<T> obj2)
        {


            if (obj1 == null && obj2 == null)
            {
                yield break;
            }

            if (obj1 == null || obj2 == null)
            {
                yield return new Difference("", DefaultValueComparer.ToString(obj1), DefaultValueComparer.ToString(obj2));
                yield break;
            }

            //if (obj1.Count != obj2.Count)
            //{
            //    yield return new Difference("Count", ((dynamic)obj1).Count.ToString(), ((dynamic)obj2).Count.ToString(),
            //            DifferenceTypes.NumberOfElementsMismatch);
            //}
            var Item1value = "";
            foreach (dynamic item1 in ((dynamic)obj1))
            {
                dynamic item2 = null;//db obj


                if (item1.GetType().GetProperty("Name") != null)
                {
                    item2 = obj2.FirstOrDefault(fi => ((dynamic)fi).Name == item1.Name);
                    Item1value = item1.Name;
                }

                else if (item1.GetType().GetProperty("FileId") != null)
                {
                    item2 = obj2.FirstOrDefault(fi => ((dynamic)fi).FileId == item1.FileId);
                    Item1value = item1.FileId;
                }

                else if (item1.GetType().GetProperty("DdsetId") != null)
                {
                    item2 = obj2.FirstOrDefault(fi => ((dynamic)fi).DdsetId == item1.DdsetId);
                    Item1value = item1.DdsetId;
                }

                else if (item1.GetType().GetProperty("Scname") != null)
                {
                    item2 = obj2.FirstOrDefault(fi => ((dynamic)fi).Scname == item1.Scname);
                    Item1value = item1.Scname;
                }

                else if (item1.GetType().GetProperty("Lstname") != null)
                {
                    item2 = obj2.FirstOrDefault(fi => ((dynamic)fi).Lstname == item1.Lstname);
                    Item1value = item1.Lstname;
                }

                else if (item1.GetType().GetProperty("TabName") != null)
                {
                    item2 = obj2.FirstOrDefault(fi => ((dynamic)fi).TabName == item1.TabName);
                    Item1value = item1.TabName;
                }

                else if (item1.GetType().GetProperty("Path") != null)
                {
                    item2 = obj2.FirstOrDefault(fi => ((dynamic)fi).Path == item1.Path);
                    Item1value = item1.Path;
                }

                if (item2 != null)
                {
                    var obj = new GetComparer<T>();
                    var comparer = obj.GetComparerobj<T>();

                    foreach (var difference in comparer.CalculateDifferences(item1, item2))
                    {
                        yield return difference.InsertPath($"[Id={item1}]");
                    }
                }
                else if (item2 == null)
                {
                    //means the obj1 is not present in obj2 i.e., not present in db so has to be added
                    yield return new Difference("MissedElementInSecondObject", Item1value, "", DifferenceTypes.MissedElementInSecondObject);
                }

            }
            foreach (dynamic item3 in ((dynamic)obj2))
            {
                dynamic item4 = null;//src obj i.e., updated data
                Item1value = "";

                if (item3.GetType().GetProperty("Name") != null)
                {
                    item4 = obj1.FirstOrDefault(fi => ((dynamic)fi).Name == item3.Name);
                    Item1value = item3.Name;
                }

                else if (item3.GetType().GetProperty("FileId") != null)
                {
                    item4 = obj1.FirstOrDefault(fi => ((dynamic)fi).FileId == item3.FileId);
                    Item1value = item3.FileId;
                }

                else if (item3.GetType().GetProperty("DdsetId") != null)
                {
                    item4 = obj1.FirstOrDefault(fi => ((dynamic)fi).DdsetId == item3.DdsetId);
                    Item1value = item3.DdsetId;
                }

                else if (item3.GetType().GetProperty("Scname") != null)
                {
                    item4 = obj1.FirstOrDefault(fi => ((dynamic)fi).Scname == item3.Scname);
                    Item1value = item3.Scname;
                }

                else if (item3.GetType().GetProperty("Lstname") != null)
                {
                    item4 = obj1.FirstOrDefault(fi => ((dynamic)fi).Lstname == item3.Lstname);
                    Item1value = item3.Lstname;
                }

                else if (item3.GetType().GetProperty("TabName") != null)
                {
                    item4 = obj1.FirstOrDefault(fi => ((dynamic)fi).TabName == item3.TabName);
                    Item1value = item3.TabName;
                }

                else if (item3.GetType().GetProperty("Path") != null)
                {
                    item4 = obj1.FirstOrDefault(fi => ((dynamic)fi).Path == item3.Path);
                    Item1value = item3.Path;
                }

                if (item4 == null)
                {
                    yield return new Difference("MissedElementInFirstObject", Item1value, "", DifferenceTypes.MissedElementInFirstObject);
                    //db obj is removed/modified by user in updated data, so this data has to be deleted in db.
                }
            }
        }



        //public class MyComparersFactory : ComparersFactory
        //{
        //    public override ObjectsComparer.IComparer<T> GetObjectsComparer<T>(ComparisonSettings settings = null,
        //        BaseComparer parentComparer = null)
        //    {
        //        //if (typeof(T) != typeof(List<T>))
        //        //{
        //        //    return base.GetObjectsComparer<T>(settings, parentComparer);
        //        //}

        //        var comparer = new CustomComparer<T>(settings, parentComparer, this);

        //        return (ObjectsComparer.IComparer<T>)comparer;

        //    }
        //}
    }
    public class GetComparer<T> where T : class
    {
        public ObjectsComparer.Comparer<T> GetComparerobj<T>() where T : class
        {
            var comparer = new ObjectsComparer.Comparer<T>();
            AddComparerOverrideParams<T>(comparer);
            return comparer;
        }

        public ObjectsComparer.Comparer<T> AddComparerOverrideParams<T>(ObjectsComparer.Comparer<T> comparer) where T : class
        {

            comparer.AddComparerOverride(typeof(long), DoNotCompareValueComparer.Instance, mem => mem.Name.ToLower().Contains("id"));
            comparer.AddComparerOverride(typeof(long?), DoNotCompareValueComparer.Instance, mem => mem.Name.ToLower().Contains("id"));
            comparer.AddComparerOverride("Cn", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("FidNavigation", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Cmp", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Calc", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Dds", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Lst", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Doc", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Pg", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Cdsd", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Sub", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("UidNavigation", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Ou", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Rsp", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Tab", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Cadst", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Fs", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("DesignDataSets", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Components", DoNotCompareValueComparer.Instance);
            //comparer.AddComparerOverride("CalculationComponentDetails", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("ResponseSubmisstionData", DoNotCompareValueComparer.Instance);
            //comparer.AddComparerOverride("TabChildDetails", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("DatasetDataDetails", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Calculations", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Documents", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Fees", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Lists", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("MetaData", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Outputs", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Pages", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("ProviderMappings", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Responses", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Sections", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("TabDetails", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("Forms", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("OrganisationDetails", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("DFChildConfigs", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("CCParentChildConfig", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("PCCParentChild", DoNotCompareValueComparer.Instance);
            comparer.AddComparerOverride("UserOrganisationDetails", DoNotCompareValueComparer.Instance);

            return comparer;
        }

    }
}

