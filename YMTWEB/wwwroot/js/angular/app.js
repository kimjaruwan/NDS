
var app = angular.module('app', []);

app.controller('MyController', function ($scope, $http) {
    // List for Dropdowns
    
    $scope.QuoData = {
        QuoNumber: '',
        CustFirstname: '',
        CustLastname: '',
        CompanyName: '',
        OrderDate: new Date().toISOString().slice(0, 10), // รูปแบบ yyyy-MM-dd
        OrderStatus: 'Processing', //กำหนดให้เป็น Processing
        ShipDate: new Date(new Date().setDate(new Date().getDate() + 3)).toISOString().slice(0, 10), // วันที่ปัจจุบัน + 3 วันในรูปแบบ yyyy-MM-dd
        TotalQty: 99,
        TotalPrice: 0,
        CustomerEmail : '',
        CustomerAddress: '',
        CustomerTaxID :'',
        CustomerAddressTax : '',
        CustomerPhone : '',
        Remark :'',
        CreateBy: 'jaruwan.s',
        CreateDate: new Date().toISOString().slice(0, 10) // รูปแบบ dd/MM/yyyy HH:mm:ss


       
    };

    $scope.ListSizes = []; 
    $scope.ListColors = []; 

    // เอาไว้ Copy ข้อมูลมาแสดงใน Table
    $scope.Entries = [];

    $scope.TotalSum = 0;

    //ng - model="NewEntry.SelectedSize" 

    // New Entry Model
    $scope.NewEntry = {
        SelectedSize: '',
        SelectedColor: '',
        Quantity: 0,
        PricePerUnit: 0,
        TotalPrice: 0
    };
    $http.post('/Home/CheckUser')
            .then(function (response) {
                $scope.userData = response.data; // เก็บข้อมูลที่ดึงมาในตัวแปร 

                console.log(response.data);
            });

    $scope.CreateQuo = function () {
        // Redirect to CreateQuo View
        window.location.href = '/Home/CreateQuo';
    };

    /*EXEC gnerateQuotationNumber*/
    $scope.GetSku = function () {
        $http.get('/Home/GetSku')
            .then(function (response) {
                $scope.ListDropSku = response.data;
                console.log("Sku Number:", response.data);
            });
        //$scope.getUserData();
        //$scope.GetOrderNos();

    };
   /* ListCompany*/

    /* $scope.GetPageLoad()*/ //เรียกใช้ฟังก์ชัน - > มีการเรียกจากหน้า ng-init="GetPageLoad()"
    $scope.GetColors = function () {
        $http.get('/Home/GetColors')
            .then(function (response) {
            $scope.ListColors = response.data;
            console.log("Colors:", response.data);
        });
    };
    $scope.GetSizes = function () {
        $http.get('/Home/GetSizes')
            .then(function (response) {
                $scope.ListSizes = response.data;
                console.log("Colors:", response.data);
            });
    };

    $scope.GetOrderType = function () {


        $http.get('/Home/GetOrderType')
            .then(function (response) {
                $scope.ListTypeSell = response.data;
                console.log("OrderTypes:", response.data);
            });

    }
    
    //// ฟังก์ชันสำหรับเลือกสี
    //$scope.SelectColor = function (color) {
    //    $scope.SelectedColor = color; // เก็บค่าสีที่เลือก
    //    console.log("Selected Color:", color);
    //};


    // Add Entry to Table
    $scope.AddEntry = function () {
        if (!$scope.NewEntry.SelectedSize || !$scope.NewEntry.SelectedColor || !$scope.NewEntry.Quantity || !$scope.NewEntry.PricePerUnit) {
       

            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Please fill in all information completely!"
               
            });
            return;
        }

        $scope.Entries.push(angular.copy($scope.NewEntry));
        //คำนวนใหม่
        $scope.CalculateTotalSum()
        $scope.NewEntry = {
            SelectedSize: '',
            SelectedColor: '',
            Quantity: 0,
            PricePerUnit: 0,
            TotalPrice: 0
        };
    };

    $scope.RemoveEntry = function (index) {
       
        if (index >= 0 && index < $scope.Entries.length) {
            $scope.Entries.splice(index, 1); 
            console.log("After removal:", $scope.Entries);
            //คำนวนใหม่
            $scope.CalculateTotalSum()
        } else {
            alert('555');
        }
    };

    $scope.TotalSum = 0;

    // ฟังก์ชันคำนวณยอดรวมทั้งหมด
    $scope.CalculateTotalSum = function () {
        $scope.TotalSum = $scope.Entries.reduce(function (sum, entry) {
            return sum + (entry.Quantity * entry.PricePerUnit);
        }, 0);
    };
    $scope.NewQuoNumber = []; 

    //GenQuotationNumber
    $scope.GenerateQuotationNumber = function () {
        $http.get('/Home/GenerateQuotationNumber')
            .then(function (response) {
                $scope.NewQuoNumber = response.data;
                console.log("QuotationNumber:", response.data);
            });

    }



    
    $scope.SaveQuotation = function (dataQuotation) {
        // ส่งข้อมูลไปยัง Backend
        console.log(dataQuotation);
        $http.post('/Home/SaveQuotation', {
            QuotationNumber: dataQuotation.QuoNumber,
            CustomerName: dataQuotation.CustFirstname,
            OrderDate: dataQuotation.OrderDate,
            OrderStatus: dataQuotation.OrderStatus,
            ShipDate: dataQuotation.ShipDate,
            TotalQty: dataQuotation.TotalQty,
    TotalPrice: dataQuotation.TotalPrice,
         CustomerEmail: dataQuotation.CustomerEmail,
     CustomerAddress: dataQuotation.CustomerAddress,
    CustomerPhone: dataQuotation.CustomerPhone,
           Remark: dataQuotation.Remark,
     CreateBy: dataQuotation.CreateBy,
         CreateDate: dataQuotation.CreateDate

            //CustomerAddressTax: dataQuotation.CustomerAddressTax,
        })
            .then(function (response) {
                alert("บันทึกข้อมูลสำเร็จแล้วจ้า");
            })
            .catch(function (error) {
                console.error("Error:", error);
                alert("ไม่สามารถบันทึกข้อมูลได้");
            });
        //Quotation type, CustLastname,CompanyName, CustomerTaxID, ที่อยู่ จังหวัด อำเภอ ...   
    }

    $scope.InsertQuotation = function () {

    }

    //$scope.InsertQuotation = function (quotations, data) {

    //    console.log(quotations);
    //    console.log(data);


    //    $scope.quotation = {
    //        OrderNumber: data.OrderNumber,
    //        OrderStatus: data.OrderStatus,
    //        CreateBy: 'Dao'
    //    }

    //    console.log($scope.quotation);

    //    $http.post('/Home/InsertQuotations', $scope.quotation).then(function (response) {
    //        console.log("Inserted:", response.data);

    //        $scope.dataQuo = response.data;

    //        alert("Data saved successfully!");
    //        $scope.quotations = {}; // ล้างฟิลด์หลังบันทึก
    //    })
    //        .catch(function (error) {
    //            console.error("Error inserting quotation:", error);
    //            alert("Failed to save data.");
    //        });




    //};

    $scope.ValidateTaxID = function () {
        //$scope.QuoData.CustomerTaxID = $scope.QuoData.CustomerTaxID.replace(/\D/g, ''); // ลบตัวอักษรที่ไม่ใช่ตัวเลข
        //if ($scope.QuoData.CustomerTaxID.length > 13) {
        //    $scope.QuoData.CustomerTaxID = $scope.QuoData.CustomerTaxID.slice(0, 13); // จำกัดความยาวไม่เกิน 13 หลัก
        //}
    };



    $scope.GetPageLoad = function () {
        $scope.GetSku() // โหลด Style Name
        $scope.GetColors() // โหลด Color
        $scope.GetSizes();     // โหลด Sizes
        $scope.GetProvince()
        $scope.GetOrderType();



 
    };


    $scope.GetProvince = function () {
        $http.get('/Home/GetProvinces')
            .then(function (response) {
                console.log(response);

                $scope.ListProvinces = response.data;
                console.log($scope.ListProvinces);
            });
    }

    $scope.GetListDistricts = function (Provincess) {

        // ส่งข้อมูลไปยัง Backend
        $http.post('/Home/GetDistricts',
            {
                Provinces: Provincess
            })
            .then(function (response) {
                console.log("Response จาก Backend:", response);
                $scope.ListDistricts = response.data; // เก็บผลลัพธ์จาก Backend
                console.log("ListDistricts:", $scope.ListDistricts);
            })
            .catch(function (error) {
                console.error("Error:", error);
            });
    }

    $scope.GetListSub = function (SelectedDistricts) {

        console.log(SelectedDistricts);
        // ส่งข้อมูลไปยัง Backend
        $http.post('/Home/GetListSubs',
            {
                Districts: SelectedDistricts
            })
            .then(function (response) {
                console.log("Response จาก Backend:", response);
                $scope.ListSub = response.data; // เก็บผลลัพธ์จาก Backend
                console.log("ListDistricts:", $scope.ListSub);
            })
            .catch(function (error) {
                console.error("Error:", error);
            });
    }
    
    $scope.GetListZipcode = function (SelectedSub) {

        console.log(SelectedSub);
        // ส่งข้อมูลไปยัง Backend
        $http.post('/Home/GetListZipcode',
            {
                SubDistricts: SelectedSub
            })
            .then(function (response) {
                $scope.SZipcode = response.data; // เก็บผลลัพธ์จาก Backend

                console.log($scope.SZipcode);
            })
            .catch(function (error) {
                console.error("Error:", error);
            });
    }

    //YmtgOrder insert

});
