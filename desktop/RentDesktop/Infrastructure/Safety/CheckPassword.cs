using System . Linq;

namespace RentDesktop . Infrastructure . Safety {
	internal class CheckPassword {
		public static string REQUIREMENTS = "Пароль должен содержать минимум " +
						$"{MIN_LEGTH} символов, минимум {MIN_UNIQUE_CHARS_COUNT} " +
						$"уникальный(-ых) символ(-ов), а также должен содержать хотя бы один символ в нижнем " +
						$"регистре, хотя бы один символ в верхнем регистре, хотя бы одну цифру и хотя бы один " +
						$"символ, не являющийся цифрой и буквой.";

		public const int MIN_LEGTH = 6;
		public const int MIN_UNIQUE_CHARS_COUNT = 1;

		public CheckPassword ( ) : this ( string . Empty ) {
			}

		public CheckPassword ( string text ) => PasswordText=text;

		public bool CheckDigit => PasswordText . Any ( t => char . IsDigit ( t ) );
		public bool CheckNonAlphanumeric => PasswordText . Any ( t => !char . IsLetterOrDigit ( t ) );

		public string PasswordText { get; }

		public bool CheckLowerCase => PasswordText . Any ( t => char . IsLower ( t ) );
		public bool CheckUpperCase => PasswordText . Any ( t => char . IsUpper ( t ) );

		public int PasswordLength => PasswordText . Length;
		public int NumOfUniqueChars => PasswordText . ToCharArray ( ) . Distinct ( ) . Count ( );

		public bool IsGood ( ) => PasswordLength>=MIN_LEGTH
						&&NumOfUniqueChars>=MIN_UNIQUE_CHARS_COUNT
						&&CheckLowerCase
						&&CheckUpperCase
						&&CheckDigit
						&&CheckNonAlphanumeric;
		}
	}
