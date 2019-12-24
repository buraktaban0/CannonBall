
public static class RandomString
{
	private static readonly string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; // + "+-*?";

	private static readonly System.Random rand = new System.Random();

	public static string GetRandomString(int length)
	{
		var stringChars = new char[length];

		for (int i = 0; i < length; i++)
		{
			stringChars[i] = chars[rand.Next(chars.Length)];
		}

		var finalString = new string(stringChars);

		return finalString;
	}

}

