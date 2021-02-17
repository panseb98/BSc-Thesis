declare
begin

for ob in (select parent.pr_Code, child.PR_BAR_CODE, child.pr_id, child.PR_CURRENT_QTY, mag.qry2 from (select * from wb_products where pr_pr_id_parent is null and PR_IS_KB_TRADE = 1 and PR_IVE_ID_ORIGINAL = 2849 and PR_IS_ACTIVE = 1 ) parent
join (select * from wb_products where pr_pr_id_parent is not null) child on child.pr_pr_id_parent = parent.pr_id 
left join (select sum(LCD_PR_AMOUNT) as qry2, LCD_PR_BARCODE from loc_localization_contents_det where lcd_is_active = 1 group by LCD_PR_BARCODE) mag on mag.LCD_PR_BARCODE = child.PR_BAR_CODE) loop 
    wb_pck_synchro.addToTestBoh(ob.PR_ID,ob.PR_CURRENT_QTY, ob.qry2, ob.PR_BAR_CODE, ob.pr_Code);
end loop;
end;
select *  from mag_cmp_qty_boh