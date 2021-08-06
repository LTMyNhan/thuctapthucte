select * from v$version;
create table TTVT(
    MaTTVT varchar(10) primary key,
    TenTTVT nvarchar2(100)
);
create table TKT(
    MaTKT varchar(10),
    TenTKT nvarchar2(100),
    MaTTVT varchar(10) REFERENCES TTVT(MaTTVT),
    PRIMARY KEY (MaTKT)
);
create table SoLieu(
    MaTKT varchar(10) REFERENCES TKT(MaTKT),
    SoLuongTBPTM int,
    SoLuongTBML int,
    SoLuongTBNH int,
    TiLeGiamTBH float,
    ThangNam date not null,
    primary key (MaTKT, ThangNam)
);

select * from TTVT;
select * from TKT;
select * from SoLieu;
--drop table TTVT;
--delete from SoLieu;

select distinct to_char(thangnam, 'MM-YYYY') from solieu;

select MaTKT, sum(SOLUONGTBPTM), to_char(thangnam, 'MM-YYYY') from SoLieu group by matkt, to_char(thangnam, 'MM-YYYY') order by to_char(thangnam, 'MM-YYYY');

select thangnam, MaTKT, sum(SOLUONGTBPTM) from SoLieu group by thangnam, MaTKT order by thangnam;

select MaTKT, sum(SOLUONGTBPTM), sum(SOLUONGTBML), sum(SOLUONGTBNH), sum(TiLeGiamTBH), thangnam  from SoLieu group by matkt, thangnam order by thangnam;

select TTVT.TenTTVT, TKT.TenTKT from TTVT join TKT on TTVT.MaTTVT = TKT.MaTTVT;

select sum(SOLUONGTBPTM), ttvt.tenttvt from SoLieu join TKT on SoLieu.MaTKT = tkt.matkt join TTVT on TKT.MaTTVT = TTVT.MaTTVT group by ttvt.tenttvt;
select sum(SOLUONGTBML), ttvt.tenttvt from SoLieu join TKT on SoLieu.MaTKT = tkt.matkt join TTVT on TKT.MaTTVT = TTVT.MaTTVT group by ttvt.tenttvt;
select sum(SOLUONGTBNH), ttvt.tenttvt from SoLieu join TKT on SoLieu.MaTKT = tkt.matkt join TTVT on TKT.MaTTVT = TTVT.MaTTVT group by ttvt.tenttvt;
select sum(TILEGIAMTBH), ttvt.tenttvt, solieu.thangnam from SoLieu join TKT on SoLieu.MaTKT = tkt.matkt join TTVT on TKT.MaTTVT = TTVT.MaTTVT group by solieu.thangnam, ttvt.tenttvt order by solieu.thangnam;

set serveroutput on;

--create or replace function get_sum_sltbptm(in_matkt varchar2)
--  return number
--is 
--    rValue number;
--begin
--  select sum(SoLuongTBPTM) into rValue from SoLieu where trim(matkt) = in_matkt;
--  return rValue;
--end;
--/

--return_value
create or replace procedure get_sum_sltbptm(in_matkt varchar2, return_value out number)
is 
begin
  select sum(SoLuongTBPTM) into return_value from SoLieu where trim(matkt) = in_matkt;
end;

--cursor
create or replace procedure get_sum_sltbptm3(in_matkt varchar2, rValue out SYS_REFCURSOR)
is 
    str varchar2(200);
begin
  str:='select *  from SoLieu where trim(matkt)='||in_matkt;
  open rValue for str;
end;

--select THUCTAP_062021.get_sum_sltbptm('BLU050100') from dual;

--select sum(SoLuongTBPTM) from SoLieu where matkt = 'BLU050100';



