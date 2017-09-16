using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Resources
{
	[GeneratedCode("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "14.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
	internal class Resource
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resource.resourceMan, null))
				{
					ResourceManager temp = new ResourceManager("Resources.Resource", Assembly.Load("App_GlobalResources"));
					Resource.resourceMan = temp;
				}
				return Resource.resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resource.resourceCulture;
			}
			set
			{
				Resource.resourceCulture = value;
			}
		}

		internal static string ClearTable
		{
			get
			{
				return Resource.ResourceManager.GetString("ClearTable", Resource.resourceCulture);
			}
		}

		internal static string TestData
		{
			get
			{
				return Resource.ResourceManager.GetString("TestData", Resource.resourceCulture);
			}
		}

		internal Resource()
		{
		}
	}
}
