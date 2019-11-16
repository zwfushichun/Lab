
--�ͻ����
CREATE PROCEDURE proc_AuditCustomer
@TempCustomerId INT, --��ʱ�ͻ�Id
@UserId INT,			 --��ǰ�û�Id
@ReturnVal VARCHAR(50) OUTPUT --�������
AS

BEGIN TRY
	BEGIN TRANSACTION
		 DECLARE @res INT EXEC @res = sp_getapplock 
		 @Resource = 'proc_AuditCustomer', @LockMode = 'Exclusive', @LockOwner = 'Transaction', @LockTimeout = 60000, @DbPrincipal = 'public' 
		 IF @res NOT IN (0, 1)	--�ж����Ƿ���Ч���Ƿ���ס��
			BEGIN
				RAISERROR ( 'Unable to acquire Lock', 16, 1 ) 
			END 
		 ELSE --�����˾Ϳ��Է�ҵ���߼���
			BEGIN   
				--���������ҵ���߼�
				--ҵ���߼�ҵ���߼�
				COMMIT TRANSACTION  
				RETURN;
				--ҵ���߼�ҵ���߼� 

				--ҵ���߼�ҵ���߼� 
			END 
	COMMIT TRANSACTION  
	SET @ReturnVal= '��˳ɹ�';
	RETURN;
END TRY

BEGIN CATCH
		ROLLBACK TRANSACTION
		SET @ReturnVal = '���ʧ��';
		RETURN;
END CATCH  
 
