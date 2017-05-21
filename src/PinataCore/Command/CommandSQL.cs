using PinataCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PinataCore.Command
{
    public class CommandSQL : ICommand
    {
        #region [ PRIVATE ]

        private IDictionary<string, bool> _tablesLoaded = new Dictionary<string, bool>();

        private IList<SampleSQLData> SetDeletionPriority(IList<SampleSQLData> childData)
        {
            foreach (var childItem in childData)
            {
                childItem.DeletePriority = (childData.Any(c => c.FK_References.Where(fk => fk.Table == childItem.Table).Count() > 0)) ? DeletePriority.Low : DeletePriority.High;
            }

            return childData.OrderByDescending(o => (int)o.DeletePriority).ToList();
        }

        #endregion

        public IList<object> CreateDelete(IList<object> list)
        {
            IList<SampleSQLData> convertedList = list.Cast<SampleSQLData>().ToList();
            IList<object> deleteList = new List<object>();

            IList<SampleSQLData> childData = SetDeletionPriority(convertedList.Where(l => l.FK_References.Count > 0).ToList());

            IList<SampleSQLData> parentData = convertedList.Where(l => l.FK_References.Count == 0).ToList();

            foreach (var child in childData)
            {
                TSQLProcessor.Execute(new CreateDeleteSQL(), child, deleteList);
            }

            foreach (var parent in parentData)
            {
                TSQLProcessor.Execute(new CreateDeleteSQL(), parent, deleteList);
            }

            return deleteList;
        }

        public IList<object> CreateInsert(IList<object> list)
        {
            IList<SampleSQLData> convertedList = list.Cast<SampleSQLData>().ToList();
            IList<object> insertList = new List<object>();

            foreach (SampleSQLData sample in convertedList)
                CreateInsertCommand(sample, insertList, convertedList);

            return insertList;
        }

        private void CreateInsertCommand(SampleSQLData sample, IList<object> insertList, IList<SampleSQLData> convertedList)
        {
            foreach (var fk in sample.FK_References)
            {
                SampleSQLData sampleDataFiltered = convertedList.SingleOrDefault(l => !_tablesLoaded.ContainsKey(fk.Table) && l.Table == fk.Table);

                if (sampleDataFiltered != null)
                    CreateInsertCommand(sampleDataFiltered, insertList, convertedList);
            }

            if (!_tablesLoaded.ContainsKey(sample.Table))
            {
                TSQLProcessor.Execute(new CreateInsertSQL(), sample, insertList);
                _tablesLoaded.Add(sample.Table, true);
            }
        }

        public IList<object> CreateUpdate(IList<object> list)
        {
            throw new NotImplementedException();
        }


        public IList<object> CreateDelete(IList<object> list, IDictionary<string, string> dynamicParameters)
        {
            IList<SampleSQLData> convertedList = list.Cast<SampleSQLData>().ToList();
            IList<object> deleteList = new List<object>();

            IList<SampleSQLData> childData = SetDeletionPriority(convertedList.Where(l => l.FK_References.Count > 0).ToList());

            IList<SampleSQLData> parentData = convertedList.Where(l => l.FK_References.Count == 0).ToList();

            foreach (var child in childData)
            {
                TSQLProcessor.Execute(new CreateDeleteSQL(), child, deleteList, dynamicParameters);
            }

            foreach (var parent in parentData)
            {
                TSQLProcessor.Execute(new CreateDeleteSQL(), parent, deleteList, dynamicParameters);
            }

            return deleteList;
        }

        public IList<object> CreateInsert(IList<object> list, IDictionary<string, string> dynamicParameters)
        {
            IList<SampleSQLData> convertedList = list.Cast<SampleSQLData>().ToList();
            IList<object> insertList = new List<object>();

            foreach (SampleSQLData sample in convertedList)
            {
                foreach (var fk in sample.FK_References)
                {
                    SampleSQLData sampleDataFiltered = convertedList.Where(l => !_tablesLoaded.ContainsKey(fk.Table) && l.Table == fk.Table).SingleOrDefault();

                    if (sampleDataFiltered != null)
                    {
                        TSQLProcessor.Execute(new CreateInsertSQL(), sampleDataFiltered, insertList, dynamicParameters);

                        _tablesLoaded.Add(fk.Table, true);
                    }
                }

                if (!_tablesLoaded.ContainsKey(sample.Table))
                {
                    TSQLProcessor.Execute(new CreateInsertSQL(), sample, insertList, dynamicParameters);

                    _tablesLoaded.Add(sample.Table, true);
                }
            }

            return insertList;
        }

        public IList<object> CreateUpdate(IList<object> list, IDictionary<string, string> dynamicParameters)
        {
            throw new NotImplementedException();
        }
    }
}
