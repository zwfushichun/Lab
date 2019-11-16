Sql 存在更新、不存在插入  不存在插入  存在更新
--方法一：
DECLARE @Id INT
SELECT @Id=Id FROM dbo.UserLastLoginBook WHERE BookId=@BookId AND UserId=@UserId
IF @Id IS NULL
	INSERT dbo.UserLastLoginBook ( BookId, UserId ) VALUES (@BookId,@UserId)
ELSE
	UPDATE dbo.UserLastLoginBook SET BookId=@BookId,UserId=@UserId WHERE Id=@Id

--方法二：
if exists(select saleId from Photo_Sale_Picture where saleId = @saleId)
UPDATE Photo_Sale_Picture 
SET SaleId=@saleId,UsingRange=@usingRange,IsPortrait=@isPortrait,MarketPrice=@marketPrice,
CopyrightPrice=@copyrightPrice,UsingPrice=@usingPrice,SystemColumnID=@systemColumnId,
PortraitImg=@portraitImg 
where SaleId=@saleId;
else 
insert into Photo_Sale_Picture(SaleId,UsingRange,IsPortrait,MarketPrice,
CopyrightPrice,UsingPrice,SystemColumnID,PortraitImg) 
values(@saleId,@usingRange,@isPortrait,@marketPrice,@copyrightPrice,
@usingPrice,@systemColumnId,@portraitImg);