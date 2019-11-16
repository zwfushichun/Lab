
--客户审核
CREATE PROCEDURE proc_AuditCustomer
@TempCustomerId INT, --临时客户Id
@UserId INT,			 --当前用户Id
@ReturnVal VARCHAR(50) OUTPUT --输出参数
AS

BEGIN TRY
	BEGIN TRANSACTION
		 DECLARE @res INT EXEC @res = sp_getapplock 
		 @Resource = 'proc_AuditCustomer', @LockMode = 'Exclusive', @LockOwner = 'Transaction', @LockTimeout = 60000, @DbPrincipal = 'public' 
		 IF @res NOT IN (0, 1)	--判断锁是否有效，是否锁住了
			BEGIN
				RAISERROR ( 'Unable to acquire Lock', 16, 1 ) 
			END 
		 ELSE --锁上了就可以放业务逻辑了
			BEGIN   
				--在这里放置业务逻辑
				--业务逻辑业务逻辑
				COMMIT TRANSACTION  
				RETURN;
				--业务逻辑业务逻辑 

				--业务逻辑业务逻辑 
			END 
	COMMIT TRANSACTION  
	SET @ReturnVal= '审核成功';
	RETURN;
END TRY

BEGIN CATCH
		ROLLBACK TRANSACTION
		SET @ReturnVal = '审核失败';
		RETURN;
END CATCH  
 
