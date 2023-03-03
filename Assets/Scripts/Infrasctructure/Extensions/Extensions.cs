using UnityEngine;

namespace Infrasctructure.Extensions
{
    public static class Extensions
    {
        public static T Deserialize<T>(this string text)
        {
            return JsonUtility.FromJson<T>(text);
        }

        public static string Serialize(this object objectToSerialize)
        {
            return JsonUtility.ToJson(objectToSerialize);
        }

        public static Vector3 ToVector3(this Vector3Data serializedVector3)
        {
            return new Vector3(serializedVector3.X, serializedVector3.Y, serializedVector3.Z);
        }

        public static Vector3Data ToSerializedVector(this Vector3 vector)
        {
            return new Vector3Data {X = vector.x, Y = vector.y, Z = vector.z};
        }
    }
}