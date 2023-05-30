using System . Linq;

namespace CarWashing . Infrastructure . Safety {
	internal class CheckPassword {
		public static string REQUIREMENTS = "Пароль должен содержать минимум " +
						$"{MIN_LEGTH} символов, минимум {MIN_UNIQUE_CHARS_COUNT} " +
						$"уникальный символ, а также должен содержать хотя бы один символ в верхнем " +
						$"регистре, хотя бы один символ в нижнем регистре, хотя бы одну цифру и хотя бы один " +
						$"символ, не являющийся цифрой и буквой (например, этим символом может быть $).";

		public const int MIN_LEGTH = 6;
		public const int MIN_UNIQUE_CHARS_COUNT = 1;

		public CheckPassword ( int debug ) : this ( string . Empty ) {
		if ( debug==1 )
			return;
			}

		public CheckPassword ( ) : this ( string . Empty ) {
			}

		public CheckPassword ( string text ) => PasswordText=text;

		public bool CheckDigit => PasswordText . Any ( t => char . IsDigit ( t ) );
		public bool CheckNotDigit => PasswordText . Any ( t => !char . IsDigit ( t ) );
		public bool CheckNonAlphanumeric => PasswordText . Any ( t => !char . IsLetterOrDigit ( t ) );
		public bool CheckNotNonAlphanumeric => PasswordText . Any ( t => char . IsLetterOrDigit ( t ) );

		public string PasswordText { get; }
		public string PasswordHiddenText { get; }

		public bool CheckLowerCase => PasswordText . Any ( t => char . IsLower ( t ) );
		public bool CheckNotLowerCase => PasswordText . Any ( t => !char . IsLower ( t ) );
		public bool CheckNotUpperCase => PasswordText . Any ( t => !char . IsUpper ( t ) );
		public bool CheckUpperCase => PasswordText . Any ( t => char . IsUpper ( t ) );

		public int PasswordLength => PasswordText . Length;
		public int PasswordHiddenLength => PasswordHiddenText . Length;
		public int NumOfUniqueChars => PasswordText . ToCharArray ( ) . Distinct ( ) . Count ( );
		public int NumOfUniqueCharsWithZero => PasswordText . ToCharArray ( ) . Distinct ( ) . Append('0') . Count ( );

		public bool IsGood ( ) => PasswordLength>=MIN_LEGTH
						&&NumOfUniqueChars>=MIN_UNIQUE_CHARS_COUNT
						&&CheckLowerCase
						&&CheckUpperCase
						&&CheckDigit
						&&CheckNonAlphanumeric;

		public bool IsNotGood ( ) => !IsGood();
		}
	}
