﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rolcore.Repository.Tests.Linq
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="RollinsIntegration")]
	public partial class TestDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertTestItem(TestItem instance);
    partial void UpdateTestItem(TestItem instance);
    partial void DeleteTestItem(TestItem instance);
    partial void InsertTestItemDetail(TestItemDetail instance);
    partial void UpdateTestItemDetail(TestItemDetail instance);
    partial void DeleteTestItemDetail(TestItemDetail instance);
    #endregion
		
		public TestDataContext() : 
				base(global::Rolcore.Repository.Tests.Properties.Settings.Default.TestConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public TestDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TestDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TestDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TestDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<TestItem> TestItems
		{
			get
			{
				return this.GetTable<TestItem>();
			}
		}
		
		public System.Data.Linq.Table<TestItemDetail> TestItemDetails
		{
			get
			{
				return this.GetTable<TestItemDetail>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TestItems")]
	public partial class TestItem : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _TestItemId;
		
		private System.Data.Linq.Binary _Timestamp;
		
		private string _NullableStringValue;
		
		private System.DateTime _DateTimeProperty;
		
		private int _IntProperty;
		
		private string _TestItemPartitionId;
		
		private EntitySet<TestItemDetail> _TestItemDetails;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnRowKeyChanging(string value);
    partial void OnRowKeyChanged();
    partial void OnTimestampChanging(System.Data.Linq.Binary value);
    partial void OnTimestampChanged();
    partial void OnStringPropertyChanging(string value);
    partial void OnStringPropertyChanged();
    partial void OnDateTimePropertyChanging(System.DateTime value);
    partial void OnDateTimePropertyChanged();
    partial void OnIntPropertyChanging(int value);
    partial void OnIntPropertyChanged();
    partial void OnPartitionKeyChanging(string value);
    partial void OnPartitionKeyChanged();
    #endregion
		
		public TestItem()
		{
			this._TestItemDetails = new EntitySet<TestItemDetail>(new Action<TestItemDetail>(this.attach_TestItemDetails), new Action<TestItemDetail>(this.detach_TestItemDetails));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TestItemId", DbType="NVarChar(250)", CanBeNull=false, IsPrimaryKey=true)]
		public override string RowKey
		{
			get
			{
				return this._TestItemId;
			}
			set
			{
				if ((this._TestItemId != value))
				{
					this.OnRowKeyChanging(value);
					this.SendPropertyChanging();
					this._TestItemId = value;
					this.SendPropertyChanged("RowKey");
					this.OnRowKeyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Timestamp", AutoSync=AutoSync.Always, DbType="rowversion NOT NULL", CanBeNull=false, IsDbGenerated=true, IsVersion=true, UpdateCheck=UpdateCheck.Never)]
		public override System.Data.Linq.Binary Timestamp
		{
			get
			{
				return this._Timestamp;
			}
			set
			{
				if ((this._Timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._Timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NullableStringValue", DbType="NVarChar(50)")]
		public override string StringProperty
		{
			get
			{
				return this._NullableStringValue;
			}
			set
			{
				if ((this._NullableStringValue != value))
				{
					this.OnStringPropertyChanging(value);
					this.SendPropertyChanging();
					this._NullableStringValue = value;
					this.SendPropertyChanged("StringProperty");
					this.OnStringPropertyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateTimeProperty", DbType="DateTime NOT NULL")]
		public override System.DateTime DateTimeProperty
		{
			get
			{
				return this._DateTimeProperty;
			}
			set
			{
				if ((this._DateTimeProperty != value))
				{
					this.OnDateTimePropertyChanging(value);
					this.SendPropertyChanging();
					this._DateTimeProperty = value;
					this.SendPropertyChanged("DateTimeProperty");
					this.OnDateTimePropertyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IntProperty", DbType="INT")]
		public override int IntProperty
		{
			get
			{
				return this._IntProperty;
			}
			set
			{
				if ((this._IntProperty != value))
				{
					this.OnIntPropertyChanging(value);
					this.SendPropertyChanging();
					this._IntProperty = value;
					this.SendPropertyChanged("IntProperty");
					this.OnIntPropertyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TestItemPartitionId", DbType="NVarChar(250)", CanBeNull=false)]
		public override string PartitionKey
		{
			get
			{
				return this._TestItemPartitionId;
			}
			set
			{
				if ((this._TestItemPartitionId != value))
				{
					this.OnPartitionKeyChanging(value);
					this.SendPropertyChanging();
					this._TestItemPartitionId = value;
					this.SendPropertyChanged("PartitionKey");
					this.OnPartitionKeyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="TestItem_TestItemDetail", Storage="_TestItemDetails", ThisKey="RowKey", OtherKey="RowKey")]
		internal EntitySet<TestItemDetail> TestItemDetails
		{
			get
			{
				return this._TestItemDetails;
			}
			set
			{
				this._TestItemDetails.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_TestItemDetails(TestItemDetail entity)
		{
			this.SendPropertyChanging();
			entity.TestItem = this;
		}
		
		private void detach_TestItemDetails(TestItemDetail entity)
		{
			this.SendPropertyChanging();
			entity.TestItem = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="")]
	public partial class TestItemDetail : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _RowKey;
		
		private string _DetailProperty;
		
		private System.Data.Linq.Binary _Timestamp;
		
		private string _PartitionKey;
		
		private EntityRef<TestItem> _TestItem;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnRowKeyChanging(string value);
    partial void OnRowKeyChanged();
    partial void OnDetailPropertyChanging(string value);
    partial void OnDetailPropertyChanged();
    partial void OnTimestampChanging(System.Data.Linq.Binary value);
    partial void OnTimestampChanged();
    partial void OnPartitionKeyChanging(string value);
    partial void OnPartitionKeyChanged();
    #endregion
		
		public TestItemDetail()
		{
			this._TestItem = default(EntityRef<TestItem>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RowKey", DbType="NVarChar(250)", CanBeNull=false, IsPrimaryKey=true)]
		public override string RowKey
		{
			get
			{
				return this._RowKey;
			}
			set
			{
				if ((this._RowKey != value))
				{
					if (this._TestItem.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnRowKeyChanging(value);
					this.SendPropertyChanging();
					this._RowKey = value;
					this.SendPropertyChanged("RowKey");
					this.OnRowKeyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DetailProperty", DbType="NVarChar(50)", CanBeNull=false, IsPrimaryKey=true)]
		public override string DetailProperty
		{
			get
			{
				return this._DetailProperty;
			}
			set
			{
				if ((this._DetailProperty != value))
				{
					this.OnDetailPropertyChanging(value);
					this.SendPropertyChanging();
					this._DetailProperty = value;
					this.SendPropertyChanged("DetailProperty");
					this.OnDetailPropertyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Timestamp", AutoSync=AutoSync.Always, DbType="rowversion NOT NULL", CanBeNull=false, IsDbGenerated=true, IsVersion=true, UpdateCheck=UpdateCheck.Never)]
		public override System.Data.Linq.Binary Timestamp
		{
			get
			{
				return this._Timestamp;
			}
			set
			{
				if ((this._Timestamp != value))
				{
					this.OnTimestampChanging(value);
					this.SendPropertyChanging();
					this._Timestamp = value;
					this.SendPropertyChanged("Timestamp");
					this.OnTimestampChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PartitionKey", DbType="NVarChar(250)", CanBeNull=false)]
		public override string PartitionKey
		{
			get
			{
				return this._PartitionKey;
			}
			set
			{
				if ((this._PartitionKey != value))
				{
					this.OnPartitionKeyChanging(value);
					this.SendPropertyChanging();
					this._PartitionKey = value;
					this.SendPropertyChanged("PartitionKey");
					this.OnPartitionKeyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="TestItem_TestItemDetail", Storage="_TestItem", ThisKey="RowKey", OtherKey="RowKey", IsForeignKey=true)]
		internal TestItem TestItem
		{
			get
			{
				return this._TestItem.Entity;
			}
			set
			{
				TestItem previousValue = this._TestItem.Entity;
				if (((previousValue != value) 
							|| (this._TestItem.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._TestItem.Entity = null;
						previousValue.TestItemDetails.Remove(this);
					}
					this._TestItem.Entity = value;
					if ((value != null))
					{
						value.TestItemDetails.Add(this);
						this._RowKey = value.RowKey;
					}
					else
					{
						this._RowKey = default(string);
					}
					this.SendPropertyChanged("TestItem");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
