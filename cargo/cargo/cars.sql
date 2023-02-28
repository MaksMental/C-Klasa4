CREATE TABLE [dbo].[cars] (
    [ID]              INT           NULL,
    [model]           NVARCHAR (50) NULL,
    [marka]           NVARCHAR (50) NULL,
    [rok_produkcji]   INT           NULL,
    [cena]            FLOAT (53)    NULL,
    [dostepnosc]      BIT           NULL,
    [typ_nadwozia]    NCHAR (10)    NULL,
    [rodzaj_paliwa]   NCHAR (10)    NULL,
    [skrzynia_biegow] NCHAR (10)    NULL,
    [moc]             NCHAR (10)    NULL,
    [silnik]          NCHAR (10)    NULL,
    [kolor]           NCHAR (10)    NULL
);

