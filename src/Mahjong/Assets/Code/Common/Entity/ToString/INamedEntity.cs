using Entitas;
using System.Text;
using UnityEngine.XR;

namespace Code.Common.Entity.ToString
{
	public interface INamedEntity : IEntity
	{
		string EntityName(IComponent[] components);
		string BaseToString();
	}
}
	