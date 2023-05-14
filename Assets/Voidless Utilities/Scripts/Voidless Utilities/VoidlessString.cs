using System.Text;
using System;

namespace VoidlessUtilities
{
public static class VoidlessString
{
	public const string ALPHABET = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"; 		/// <summary>Abecedary.</summary>
	public const string PATH_ROOT_VOIDLESS_UTILITIES = "Voidless Utilties"; 								/// <summary>Voidless Utilities' Root.</summary>
	public const string PATH_SCRIPTABLE_OBJECTS = PATH_ROOT_VOIDLESS_UTILITIES + "/Scriptable Objects"; 	/// <summary>Scriptable Objects' Path.</summary>

	/// <summary>Sets string to Camel Case format.</summary>
	/// <param name="_text">Text to format to Camel Case.</param>
	/// <returns>Formated text.</returns>
	public static string ToCamelCase(this string _text)
	{
		return _text.Replace(_text[0], char.ToLower(_text[0]));
	}

	/// <summary>Gives a string, replacing all instances of chars into a new char.</summary>
	/// <param name="_text">String to replace chars to.</param>
	/// <param name="_from">Char instance to replace.</param>
	/// <param name="_to">Char to substitute.</param>
	/// <returns>String with chars replaced.</returns>
	public static string WithReplacedChars(this string _text, char _from, char _to)
	{
		StringBuilder result = new StringBuilder();

		for(int i = 0; i < _text.Length; i++)
		{
			result.Append((_text[i] == _from) ? _to : _text[i]);
		}

		return result.ToString();
	}

	/// <summary>Replaces all chars' instances of a string into a new char.</summary>
	/// <param name="_text">String to replace chars to.</param>
	/// <param name="_from">Char instance to replace.</param>
	/// <param name="_to">Char to substitute.</param>
	public static void ReplaceChars(ref string _text, char _from, char _to)
	{
		StringBuilder result = new StringBuilder();

		for(int i = 0; i < _text.Length; i++)
		{
			result.Append((_text[i] == _from) ? _to : _text[i]);
		}

		_text = result.ToString();
	}

	public static string GenerateRandomString(int length, string _string = ALPHABET)
	{
		Random random = new Random();
		StringBuilder result = new StringBuilder(length);
		for(int i = 0; i < length; i++)
		{
			result.Append(_string[random.Next(_string.Length)]);
		}

		return result.ToString();
	}

	/// <summary>Converts Snake Case Text to Spaced Case.</summary>
	/// <param name="_text">Text to convert.</param>
	/// <returns>Text with spaces instead of underscores.</returns>
	public static string SnakeCaseToSpacedText(this string _text)
	{
		return _text.Replace("_", " ");
	}

	/// <summary>Creates a string of characters repeated n times.</summary>
	/// <param name="_character">Character to repeat.</param>
	/// <returns>String of characters repeated n times.</returns>
	public static string CharactersPeriodically(char _character, int _count)
	{
		StringBuilder builder = new StringBuilder();

		for(int i = 0; i < _count; i++)
		{
			builder.Append(_character);
		}

		return builder.ToString();
	}

	/// <summary>Creates a string of strings repeated n times.</summary>
	/// <param name="_character">Character to repeat.</param>
	/// <returns>String of strings repeated n times.</returns>
	public static string StringsPeriodically(string _text, int _count)
	{
		StringBuilder builder = new StringBuilder();

		for(int i = 0; i < _count; i++)
		{
			builder.Append(_text);
		}

		return builder.ToString();
	}
}
}