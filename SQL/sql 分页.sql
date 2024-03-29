USE [TestDB]
GO
/****** Object:  StoredProcedure [dbo].[P_GridViewPager]    Script Date: 2014-09-04 17:53:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[P_GridViewPager] (
    @recordTotal INT OUTPUT,            --输出记录总数
    @viewName NVARCHAR(MAX),            --表名（了可以多表联合查询、如果是多变在这里写多变的关联关系）
    @fieldName NVARCHAR(MAX) = '*',     --查询字段
    @keyName VARCHAR(max) = 'Id',       --索引字段
    @pageSize INT = 20,                 --每页记录数
    @pageNo INT =1,                     --当前页
    @orderString NVARCHAR(MAX),         --排序条件
    @whereString NVARCHAR(MAX) = '1=1'  --WHERE条件
)
AS
BEGIN
     DECLARE @beginRow INT  --开始行
     DECLARE @endRow INT    --结束行
     DECLARE @tempLimit VARCHAR(max)
     DECLARE @tempCount NVARCHAR(max)
     DECLARE @tempMain VARCHAR(max)

     SET @beginRow = (@pageNo - 1) * @pageSize    + 1
     SET @endRow = @pageNo * @pageSize
     SET @tempLimit = 'rows BETWEEN ' + CAST(@beginRow AS VARCHAR) +' AND '+CAST(@endRow AS VARCHAR)   
     --输出参数为总记录数
     SET @tempCount = 'SELECT @recordTotal = COUNT(*) FROM (SELECT '+@keyName+' FROM '+@viewName+' WHERE '+@whereString+') AS my_temp'
     EXECUTE sp_executesql @tempCount,N'@recordTotal INT OUTPUT',@recordTotal OUTPUT
       
     --主查询返回结果集
     SET @tempMain = 'SELECT * FROM (SELECT ROW_NUMBER() OVER ( order by '+@orderString+') AS rows ,'+@fieldName+' FROM '+@viewName+' WHERE '+@whereString+') AS main_temp WHERE '+@tempLimit  
     --PRINT @tempMain
     EXECUTE (@tempMain)
END



--使用方法：单表分页
EXEC	@return_value = [dbo].[P_GridViewPager]
		@recordTotal = @recordTotal OUTPUT,
		@viewName = N'LargeTable',
		@fieldName = N'*',
		@keyName = N'ID',
		@pageSize = 100,
		@pageNo = 1,
		@orderString='ID'

--多表联合查询分页
EXEC	@return_value = [dbo].[P_GridViewPager]
		@recordTotal = @recordTotal OUTPUT,
		@viewName = N'child LEFT JOIN parent ON child.ParentId=parent.id',
		@fieldName = N'child.Parentid',
		@keyName = N'child.ID',
		@pageSize = 10,
		@pageNo = 1,
		@orderString='child.ID',
		@whereString=' and parent.name=123'