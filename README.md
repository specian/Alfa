# Domácí úkol – řešení
Zadání:
https://gist.github.com/remunda/3b510f54c6d11a2011bc
nebo
https://bitbucket.org/continero/backend_homework/src/master/

Komentáře k původnímu kódu podle zadání:

1. Kód není přeložitelný, neměl by být v tomto stavu pushnutý do repa,
2. soubor obsahuje dvě třídy, standardem je mít jednu třídu v jednom souboru,
3. obsahuje „zadrátované“ cesty k souborům – ty by měly být v konfigu nebo jako parametry,
4. v try-catch bloku se vyhazuje nově vytvořená výjimka, tím se nepředává informace o aktuální výjimce kromě message; měla by se přeposlat dál pouze příkazem „throw“, což v tomto kódu ztrácí smysl, takže try-catch je tam zbytečný;
5. proměnná „input“ se deklaruje v bloku „catch“, takže za ním už není viditelná, proto kód není přeložitelný;
6. StreamReader je IDisposable, tedy měl by být v „using“ bloku, aby se hned po jeho použití uvolnily jím alokované zdroje;
7. stejně tak FileStream;
8. ale FileStream není třeba, protože stačí použít metodu File.ReadAllText();
9. dále by bylo vhodnější použít asynchronní verzi metody, aby se program neblokoval než skončí IO operace, tedy ve výsledku
string input = await File.ReadAllTextAsync(sourceFileName);
tím se celý původní blok try-catch zredukoval na jednu řádku;
10. v třídě Document bych změnil nastavení properties z { get; set; } na { get; init; }, protože po vytvoření objektu je zřejmě už nebudeme chtít měnit;
11. pokud je překladač nastavený jako „nullable“ (což by v novém kódu mělo být), tak properties třídy Document vyhazují warning, protože nejsou označeny jako nullable;
12. není ošetřený nesprávně formátovaný XML dokument nebo chybějící elementy;
13. na výstupu, podobně na jako na vstupu, není potřeba zakládat stream, ale stačí File.WriteAllText.
