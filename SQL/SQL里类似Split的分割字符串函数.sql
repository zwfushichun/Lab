/*
 *SQL对字符串的处理能力比较弱，比如我要循环遍历象1,2,3,4,5这样的字符串，如果用数组的话，遍历很简单，但是T-SQL不支持数组，所以处理下来比较麻烦。
 *下边的函数，实现了象数组一样去处理字符串。
*/

-- 1、按照指定的标记分离字符串，并返回分割后的表 
-- =============================================
-- Author:		张伟
-- Create date: 2015-04-23
-- Description:	按照指定的标记分离字符串
-- =============================================
CREATE FUNCTION [dbo].[fun_Split](@Str VARCHAR(2000),@Split VARCHAR(2)) 
RETURNS @t TABLE(col VARCHAR(20))
AS
BEGIN
	WHILE(CHARINDEX(@Split,@Str)<>0)
	BEGIN
		INSERT @t(col) VALUES(SUBSTRING(@Str,1,CHARINDEX(@Split,@Str)-1))
		SET @Str =STUFF(@Str,1,CHARINDEX(@Split,@Str),'')
	END
    INSERT @t(col) VALUES(@Str)
	return   
END

--2、按指定符号分割字符串，返回分割后的元素个数，方法很简单，就是看字符串中存在多少个分隔符号，然后再加一，就是要求的结果
CREATE function Get_StrArrayLength 
( 
@str varchar(1024), --要分割的字符串 
@split varchar(10) --分隔符号 
) 
returns int 
as 
begin 
declare @location int 
declare @start int 
declare @length int 

set @str=ltrim(rtrim(@str)) 
set @location=charindex(@split,@str) 
set @length=1 
while @location<>0 
begin 
set @start=@location+1 
set @location=charindex(@split,@str,@start) 
set @length=@length+1 
end 
return @length 
end 
/*
调用示例：select dbo.Get_StrArrayLength('78,1,2,3',',') 
返回值：4 
 */

 --3、按指定符号分割字符串，返回分割后指定索引的第几个元素，象数组一样方便 
 CREATE function Get_StrArrayStrOfIndex 
( 
@str varchar(1024), --要分割的字符串 
@split varchar(10), --分隔符号 
@index int --取第几个元素 
) 
returns varchar(1024) 
as 
begin 
declare @location int 
declare @start int 
declare @next int 
declare @seed int 

set @str=ltrim(rtrim(@str)) 
set @start=1 
set @next=1 
set @seed=len(@split) 

set @location=charindex(@split,@str) 
while @location<>0 and @index>@next 
begin 
set @start=@location+@seed 
set @location=charindex(@split,@str,@start) 
set @next=@next+1 
end 
if @location =0 select @location =len(@str)+1 
--这儿存在两种情况：1、字符串不存在分隔符号 2、字符串中存在分隔符号，跳出while循环后，@location为0，那默认为字符串后边有一个分隔符号。 

return substring(@str,@start,@location-@start) 
end 
/*
调用示例：select dbo.Get_StrArrayStrOfIndex('8,9,4',',',2) 
返回值：9 
*/

--4、结合上边两个函数，象数组一样遍历字符串中的元素
declare @str varchar(50) 
set @str='1,2,3,4,5' 
declare @next int 
set @next=1 
while @next<=dbo.Get_StrArrayLength(@str,',') 
begin 
print dbo.Get_StrArrayStrOfIndex(@str,',',@next) 
set @next=@next+1 
end 
/*
调用结果： 
1 
2 
3 
4 
5 
*/