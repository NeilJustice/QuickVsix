using System;
using System.Reflection;

namespace CSharpUtils
{
   public static class Reflect
   {
      public static FieldInfo GetFieldInfo<T>(string fieldName)
      {
         Type type = typeof(T);
         return GetFieldInfo(type, fieldName);
      }

      public static FieldInfo GetFieldInfo(Type type, string fieldName)
      {
         FieldInfo instanceField = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
         ThrowIfNullFieldInfo(instanceField, type.FullName, fieldName);
         return instanceField;
      }

      public static object Get(object obj, string fieldName)
      {
         Type objectType = obj.GetType();
         FieldInfo instanceField = GetFieldInfo(objectType, fieldName);
         ThrowIfNullFieldInfo(instanceField, objectType.FullName, fieldName);
         object fieldValue = instanceField.GetValue(obj);
         return fieldValue;
      }

      public static T Get<T>(object obj, string fieldName)
      {
         T instanceField = (T)Get(obj, fieldName);
         return instanceField;
      }

      public static void Set<ValueType>(object obj, string fieldName, ValueType value)
      {
         Type objectType = obj.GetType();
         FieldInfo instanceField = GetFieldInfo(objectType, fieldName);
         ThrowIfNullFieldInfo(instanceField, objectType.FullName, fieldName);
         Type valueType = value.GetType();
         if (!instanceField.FieldType.IsAssignableFrom(valueType))
         {
            string exceptionMessage = $"Field {objectType.FullName}.{fieldName} with type {instanceField.FieldType} is not assignable from value [{value}] which is of type {value.GetType().FullName}";
            throw new ArgumentException(exceptionMessage);
         }
         instanceField.SetValue(obj, value);
      }

      public static void SetProperty<ValueType>(object obj, string propertyName, ValueType value)
      {
         Type objectType = obj.GetType();
         PropertyInfo propertyInfo = objectType.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
         if (propertyInfo == null)
         {
            throw new ArgumentException($"Property not found on object: {propertyName}");
         }
         propertyInfo.SetValue(obj, value);
      }

      public static void ThrowIfNullFieldInfo(FieldInfo fieldInfo, string fullTypeName, string fieldName)
      {
         if (fieldInfo == null)
         {
            string exceptionMessage = $"Field not found: {fullTypeName}.{fieldName}";
            throw new ArgumentException(exceptionMessage);
         }
      }
   }
}
