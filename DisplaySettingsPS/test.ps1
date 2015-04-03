
# init
add-pssnapin displaysettingscmdlets
get-pssnapin display*
##################################

#query	
echo "get-displaysettings ***** ***** ***** ***** ***** ***** "
get-displaysettings 


#query all
echo "get-displaysettings all ***** ***** ***** ***** ***** ***** "
get-displaysettings -all  


#set 
echo "set-displaysettings ... ***** ***** ***** ***** ***** ***** "
set-displaysettings 1280 800  

set-displaysettings 1280 8999  


#set error


#set ok


##################################


# cleanup


remove-pssnapin displaysettingscmdlets

