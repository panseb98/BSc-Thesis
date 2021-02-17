declare 
o_completions tcompletions;
o_message varchar(266);
begin
    wb_pck_mobile_sync.get_completions (81100,
                              18004,
                              o_completions      ,
                              o_message          ,
                              o_message          );
                              
                              
                                        wb_pck_mobile_sync.log_mobi (1   ,
                     'asdasdasd' ,
                       'aaaaaa',
                       'aaaaaa');
       for v_i in 1..o_completions.count() loop
          wb_pck_mobile_sync.log_mobi (1   ,
                       o_completions(v_i).print_number,
                       'aaaaaa',
                       'aaaaaa');
       end loop;                       
end;

select * from mobi_log order by mobi_id desc