using Microsoft.EntityFrameworkCore.Migrations;

namespace SkipperAgency.Infrastructure.Persistence.Migrations
{
    public partial class LanguagesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkipperLanguage_Languages_LanguageId",
                table: "SkipperLanguage");

            migrationBuilder.Sql("TRUNCATE TABLE Languages");
            migrationBuilder.Sql("DBCC CHECKIDENT ('[Languages]', RESEED, 0);");

            migrationBuilder.Sql(
@"INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ab','Abkhazian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ae','Avestan');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('af','Afrikaans');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ak','Akan');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('am','Amharic');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('an','Aragonese');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ar','Arabic');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('as','Assamese');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('av','Avaric');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ay','Aymara');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('az','Azerbaijani');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ba','Bashkir');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('be','Belarusian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('bg','Bulgarian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('bh','Bihari languages');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('bi','Bislama');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('bm','Bambara');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('bn','Bengali');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('bo','Tibetan');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('br','Breton');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('bs','Bosnian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ca','Catalan; Valencian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ce','Chechen');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ch','Chamorro');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('co','Corsican');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('cr','Cree');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('cs','Czech');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('cu','Church Slavic; Old Slavonic; Church Slavonic; Old Bulgarian; Old Church Slavonic');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('cv','Chuvash');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('cy','Welsh');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('da','Danish');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('de','German');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('dv','Divehi; Dhivehi; Maldivian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('dz','Dzongkha');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ee','Ewe');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('el','Greek, Modern (1453-)');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('en','English');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('eo','Esperanto');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('es','Spanish; Castilian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('et','Estonian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('eu','Basque');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('fa','Persian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ff','Fulah');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('fi','Finnish');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('fj','Fijian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('fo','Faroese');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('fr','French');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('fy','Western Frisian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ga','Irish');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('gd','Gaelic; Scottish Gaelic');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('gl','Galician');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('gn','Guarani');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('gu','Gujarati');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('gv','Manx');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ha','Hausa');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('he','Hebrew');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('hi','Hindi');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ho','Hiri Motu');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('hr','Croatian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ht','Haitian; Haitian Creole');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('hu','Hungarian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('hy','Armenian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('hz','Herero');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ia','Interlingua (International Auxiliary Language Association)');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('id','Indonesian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ie','Interlingue; Occidental');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ig','Igbo');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ii','Sichuan Yi; Nuosu');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ik','Inupiaq');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('io','Ido');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('is','Icelandic');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('it','Italian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('iu','Inuktitut');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ja','Japanese');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('jv','Javanese');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ka','Georgian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('kg','Kongo');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ki','Kikuyu; Gikuyu');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('kj','Kuanyama; Kwanyama');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('kk','Kazakh');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('kl','Kalaallisut; Greenlandic');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('km','Central Khmer');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('kn','Kannada');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ko','Korean');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('kr','Kanuri');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ks','Kashmiri');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ku','Kurdish');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('kv','Komi');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('kw','Cornish');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ky','Kirghiz; Kyrgyz');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('la','Latin');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('lb','Luxembourgish; Letzeburgesch');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('lg','Ganda');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('li','Limburgan; Limburger; Limburgish');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ln','Lingala');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('lo','Lao');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('lt','Lithuanian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('lu','Luba-Katanga');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('lv','Latvian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('mg','Malagasy');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('mh','Marshallese');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('mi','Maori');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('mk','Macedonian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ml','Malayalam');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('mn','Mongolian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('mr','Marathi');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ms','Malay');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('mt','Maltese');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('my','Burmese');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('na','Nauru');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('nb','Bokmål, Norwegian; Norwegian Bokmål');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('nd','Ndebele, North; North Ndebele');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ne','Nepali');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ng','Ndonga');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('nl','Dutch; Flemish');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('nn','Norwegian Nynorsk; Nynorsk, Norwegian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('no','Norwegian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('nr','Ndebele, South; South Ndebele');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('nv','Navajo; Navaho');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ny','Chichewa; Chewa; Nyanja');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('oc','Occitan (post 1500); Provençal');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('oj','Ojibwa');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('om','Oromo');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('or','Oriya');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('os','Ossetian; Ossetic');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('pa','Panjabi; Punjabi');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('pi','Pali');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('pl','Polish');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ps','Pushto; Pashto');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('pt','Portuguese');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('qu','Quechua');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('rm','Romansh');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('rn','Rundi');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ro','Romanian; Moldavian; Moldovan');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ru','Russian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('rw','Kinyarwanda');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('sa','Sanskrit');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('sc','Sardinian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('sd','Sindhi');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('se','Northern Sami');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('sg','Sango');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('si','Sinhala; Sinhalese');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('sk','Slovak');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('sl','Slovenian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('sm','Samoan');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('sn','Shona');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('so','Somali');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('sq','Albanian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('sr','Serbian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ss','Swati');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('st','Sotho, Southern');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('su','Sundanese');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('sv','Swedish');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('sw','Swahili');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ta','Tamil');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('te','Telugu');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('tg','Tajik');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('th','Thai');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ti','Tigrinya');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('tk','Turkmen');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('tl','Tagalog');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('tn','Tswana');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('to','Tonga (Tonga Islands)');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('tr','Turkish');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ts','Tsonga');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('tt','Tatar');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('tw','Twi');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ty','Tahitian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ug','Uighur; Uyghur');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('uk','Ukrainian');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ur','Urdu');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('uz','Uzbek');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('ve','Venda');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('vi','Vietnamese');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('vo','Volapük');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('wa','Walloon');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('wo','Wolof');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('xh','Xhosa');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('yi','Yiddish');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('yo','Yoruba');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('za','Zhuang; Chuang');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('zh','Chinese');
INSERT INTO Languages(TwoLetterCode,EnglishName) VALUES ('zu','Zulu');");

            migrationBuilder.AddForeignKey(
                name: "FK_SkipperLanguage_Languages_LanguageId",
                table: "SkipperLanguage",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SkipperLanguage_Languages_LanguageId",
                table: "SkipperLanguage");

            migrationBuilder.Sql("TRUNCATE TABLE Languages");

            migrationBuilder.AddForeignKey(
                name: "FK_SkipperLanguage_Languages_LanguageId",
                table: "SkipperLanguage",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

        }
    }
}
