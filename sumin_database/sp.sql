USE [sumin]
GO

/****** Object:  StoredProcedure [dbo].[sp_collectprocessdata]    Script Date: 2019/7/30 9:59:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<susan>
-- Create date: <2019-07-26>
-- Description:	<采集数据放入db>
-- =============================================
CREATE PROCEDURE [dbo].[sp_collectprocessdata] 
	-- Add the parameters for the stored procedure here
	@str varchar(max),
	@msg varchar(max) output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	create table #t(
		SonStr nvarchar(1000)
	)
	insert into #t
	(SonStr)
	select SonStr from dbo.SplitStr(@str,';');
	--select * from #t;

	--#t中的每一行都按照‘，’分隔开
	declare @rowStr nvarchar(500);
	declare @original nvarchar(500);
	create table #temp(
		PLCAddress nvarchar(100),
		CollectTime datetime,
		CollectValue decimal(18,2)
	)

	declare @PLCAddress nvarchar(100);
	declare @CollectTime datetime;
	declare @CollectValue decimal(18,2);
select * from SM_T_PROCESS_COLLECT
	while exists (select 1 SonStr from #t)
	begin
		select top 1 @rowStr=SonStr from #t;
		set @original=@rowStr;
		
		print @rowStr;
		if charindex('||',@rowStr)<>0
		begin
          set @PLCAddress=substring(@rowStr,1,charindex('||',@rowStr)-1);
		  
		  if LEFT(@PLCAddress,5)='ERROR'
		   begin
			insert into SM_T_PROCESS_COLLECT
		     (PKId,PLCAddress,CollectTime)
		     values
		    (replace(newid(), '-', '') ,substring(@PLCAddress,5,len(@PLCAddress)-5),getdate());
		     return;
			 end
		  
		  set @rowStr = stuff(@rowStr,1,charindex('||',@rowStr)+1,'') 
		end
		print @PLCAddress;
		if charindex('||',@rowStr)<>0
		begin
          set @CollectTime=cast(substring(@rowStr,1,charindex('||',@rowStr)-1) as datetime);
		  set @rowStr = stuff(@rowStr,1,charindex('||',@rowStr)+1,'') 
		end
		print @CollectTime;
		--if charindex('||',@rowStr)<>0
		--begin
		set @CollectValue=@rowStr;
    --      set @CollectValue=substring(@rowStr,1,charindex('||',@rowStr)-1);
		  --set @rowStr = stuff(@rowStr,1,charindex('||',@rowStr)+1,'') 
		--end
		print @CollectValue;
		insert into SM_T_PROCESS_COLLECT
		(PKId,PLCAddress,CollectTime,collectValue)
		values
		(replace(newid(), '-', '') ,@PLCAddress,@CollectTime,@CollectValue);

		delete from #t where SonStr=@original;
	end

	--select * from #temp;

	set @msg=N'OK';
	return;
	
END
GO


