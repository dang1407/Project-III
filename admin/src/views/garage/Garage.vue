<template>
  <div class="bg-[#e0e0e0] w-[100%] h-[100%]">
    <div class="h-[60px] flex justify-center items-center">
      <div class="w-[90%]">
        <h1>Bản đồ bãi đỗ xe</h1>
      </div>
    </div>
    <div class="flex justify-center">
      <div class="rounded-[10px] bg-[#fff] p-[24px] w-[90%] h-[600px]">
        <div class="border-[2px] border-[#ccc] rounded-[4px] h-[100%]">
          <div class="flex h-[100%] justify-between">
            <!-- Đặt điều kiện v-show để tránh chớp các cột khi chưa get xong API -->
            <div class="flex h-[100%]" v-show="parkSlotsObject.A1">
              <div class="flex flex-col justify-between">
                <div class="">
                  <div class="car__box1 flex">
                    <div
                      v-for="(element, index) in parkSlotData.E1"
                      @click="
                        element.ParkSlotState == 0
                          ? toggleGarageForm('2', element)
                          : toggleGarageOutForm(
                              element.LicensePlate,
                              element.ParkSlotCode
                            )
                      "
                      class="car__slot cursor-pointer center_image"
                    >
                      <img
                        v-if="element.ParkSlotState == 1"
                        src="../../assets/imgs/top_view_car_icon.png"
                        alt=""
                      />
                    </div>
                    <div class="w-[50px] flex flex-col items-center mx-[12px]">
                      <div class="">Cột E</div>
                      <div>
                        <div class="column__icon"></div>
                      </div>
                    </div>
                    <div
                      v-for="(element, index) in parkSlotData.E2"
                      @click="
                        element.ParkSlotState == 0
                          ? toggleGarageForm('2', element)
                          : toggleGarageOutForm(
                              element.LicensePlate,
                              element.ParkSlotCode
                            )
                      "
                      class="car__slot cursor-pointer center_image"
                    >
                      <img
                        v-if="element.ParkSlotState == 1"
                        src="../../assets/imgs/top_view_car_icon.png"
                        alt=""
                      />
                    </div>
                    <!-- <div class="w-[32px] h-[64px]"></div> -->
                  </div>
                  <!-- <div class="flex items-center justify-center pt-[4px]">
                    <div class="column__icon mr-[4px]"></div>
                    <div>Cột A</div>
                  </div> -->
                  <div class="flex">
                    <!-- Begin B1 -->
                    <div class="motorbike__box mt-[18px]">
                      <div
                        v-for="(element, index) in parkSlotData.B1"
                        @click="
                          element.ParkSlotState == 0
                            ? toggleGarageForm('1', element)
                            : toggleGarageOutForm(
                                element.LicensePlate,
                                element.ParkSlotCode
                              )
                        "
                        class="motorbike__slot--horizontal cursor-pointer center_image"
                      >
                        <div class="flex justify-center w-[100%] h-[100%]">
                          <img
                            v-if="element.ParkSlotState == 1"
                            class="-rotate-90"
                            src="../../assets/imgs/top_view_motorbike_icon.png"
                            alt=""
                          />
                        </div>
                      </div>
                    </div>
                    <!-- End B1 -->
                    <div class="flex flex-col justify-center items-center">
                      <div class="-rotate-90">Cột B</div>
                      <div class="column__icon mt-[12px]"></div>
                    </div>
                    <!-- Begin B2 -->
                    <div class="motorbike__box mt-[18px]">
                      <div
                        v-for="(element, index) in parkSlotData.B2"
                        @click="
                          element.ParkSlotState == 0
                            ? toggleGarageForm('1', element)
                            : toggleGarageOutForm(
                                element.LicensePlate,
                                element.ParkSlotCode
                              )
                        "
                        class="motorbike__slot--horizontal cursor-pointer center_image"
                      >
                        <div class="flex justify-center w-[100%] h-[100%]">
                          <img
                            v-if="element.ParkSlotState == 1"
                            class="-rotate-90"
                            src="../../assets/imgs/top_view_motorbike_icon.png"
                            alt=""
                          />
                        </div>
                      </div>
                    </div>
                    <!-- End B2 -->
                    <!-- Begin B3 -->
                    <div class="motorbike__box mt-[18px]">
                      <div
                        v-for="(element, index) in parkSlotData.B3"
                        @click="
                          element.ParkSlotState == 0
                            ? toggleGarageForm('1', element)
                            : toggleGarageOutForm(
                                element.LicensePlate,
                                element.ParkSlotCode
                              )
                        "
                        class="motorbike__slot--horizontal cursor-pointer center_image"
                      >
                        <div class="flex justify-center w-[100%] h-[100%]">
                          <img
                            v-if="element.ParkSlotState == 1"
                            class="-rotate-90"
                            src="../../assets/imgs/top_view_motorbike_icon.png"
                            alt=""
                          />
                        </div>
                      </div>
                    </div>
                    <!-- End B3 -->
                    <!-- Begin B4 -->
                    <!-- <div class="motorbike__box mt-[18px]">
                      <div class="motorbike__slot--horizontal cursor-pointer center_image">
                        <div class="flex justify-center w-[100%] h-[100%]">
                          <img
                            v-if="element.ParkSlotState == 1"
                            class="-rotate-90"
                            src="../../assets/imgs/top_view_motorbike_icon.png"
                            alt=""
                          />
                        </div>
                      </div>
                    </div> -->
                    <!-- End B4 -->
                    <!-- Begin Cột C -->
                    <div class="flex flex-col justify-center items-center">
                      <div class="-rotate-90">Cột C</div>
                      <div class="column__icon mt-[12px]"></div>
                    </div>
                    <!-- End Cột C -->
                    <!-- Begin C1 -->
                    <div class="motorbike__box mt-[18px]">
                      <div
                        v-for="(element, index) in parkSlotData.C1"
                        @click="
                          element.ParkSlotState == 0
                            ? toggleGarageForm('1', element)
                            : toggleGarageOutForm(
                                element.LicensePlate,
                                element.ParkSlotCode
                              )
                        "
                        class="motorbike__slot--horizontal cursor-pointer center_image"
                      >
                        <div class="flex justify-center w-[100%] h-[100%]">
                          <img
                            v-if="element.ParkSlotState == 1"
                            class="-rotate-90"
                            src="../../assets/imgs/top_view_motorbike_icon.png"
                            alt=""
                          />
                        </div>
                      </div>
                    </div>
                    <!-- End C1 -->
                    <!-- Begin C2 -->
                    <div class="motorbike__box mt-[18px]">
                      <div
                        v-for="(element, index) in parkSlotData.C2"
                        @click="
                          element.ParkSlotState == 0
                            ? toggleGarageForm('1', element)
                            : toggleGarageOutForm(
                                element.LicensePlate,
                                element.ParkSlotCode
                              )
                        "
                        class="motorbike__slot--horizontal cursor-pointer center_image"
                      >
                        <div class="flex justify-center w-[100%] h-[100%]">
                          <img
                            v-if="element.ParkSlotState == 1"
                            class="-rotate-90"
                            src="../../assets/imgs/top_view_motorbike_icon.png"
                            alt=""
                          />
                        </div>
                      </div>
                    </div>
                    <!-- End C2 -->
                    <!-- Begin C3 -->
                    <div class="bikecycle__box flex mt-[18px]">
                      <div>
                        <div
                          v-for="(element, index) in parkSlotData.C3"
                          @click="
                            element.ParkSlotState == 0
                              ? toggleGarageForm('0', element)
                              : toggleGarageOutForm(null, element.ParkSlotCode)
                          "
                          class="bikecycle__slot--horizontal center_image p-[4px]"
                        >
                          <div
                            class="flex items-center justify-center w-[100%] h-[100%]"
                          >
                            <img
                              v-if="element.ParkSlotState == 1"
                              class="-rotate-90 h-[32px]"
                              src="../../assets/imgs/top_view_bikecycle_icon.png"
                              alt=""
                            />
                          </div>
                        </div>
                      </div>
                      <div>
                        <div
                          v-for="(element, index) in parkSlotData.C4"
                          @click="
                            element.ParkSlotState == 0
                              ? toggleGarageForm('0', element)
                              : toggleGarageOutForm(null, element.ParkSlotCode)
                          "
                          class="bikecycle__slot--horizontal center_image p-[4px]"
                        >
                          <div
                            class="flex items-center justify-center w-[100%] h-[100%]"
                          >
                            <img
                              v-if="element.ParkSlotState == 1"
                              class="-rotate-90 h-[32px]"
                              src="../../assets/imgs/top_view_bikecycle_icon.png"
                              alt=""
                            />
                          </div>
                        </div>
                      </div>
                    </div>
                    <!-- End C3 -->
                    <!-- <div class="invisible">Thang máy</div> -->
                  </div>
                  <div class="flex">
                    <!-- Begin A1 -->
                    <div class="motorbike__box mt-[18px]">
                      <div
                        v-for="(element, index) in parkSlotData.A1"
                        @click="toggleGarageForm('1', element)"
                        class="motorbike__slot--horizontal cursor-pointer center_image"
                      >
                        <div class="flex justify-center w-[100%] h-[100%]">
                          <img
                            v-if="element.ParkSlotState == 1"
                            class="-rotate-90"
                            src="../../assets/imgs/top_view_motorbike_icon.png"
                            alt=""
                          />
                        </div>
                      </div>
                    </div>
                    <!-- End A1 -->
                    <div class="flex flex-col justify-center items-center">
                      <div class="-rotate-90">Cột A</div>
                      <div class="column__icon mt-[12px]"></div>
                    </div>
                    <!-- Begin A2 -->
                    <div class="motorbike__box mt-[18px]">
                      <div
                        v-for="(element, index) in parkSlotData.A2"
                        @click="toggleGarageForm('1', element)"
                        class="motorbike__slot--horizontal cursor-pointer center_image"
                      >
                        <div class="flex justify-center w-[100%] h-[100%]">
                          <img
                            v-if="element.ParkSlotState == 1"
                            class="-rotate-90"
                            src="../../assets/imgs/top_view_motorbike_icon.png"
                            alt=""
                          />
                        </div>
                      </div>
                    </div>
                    <!-- End A2 -->
                    <!-- Begin A3 -->
                    <div class="motorbike__box mt-[18px]">
                      <div
                        v-for="(element, index) in parkSlotData.A3"
                        @click="toggleGarageForm('1', element)"
                        class="motorbike__slot--horizontal cursor-pointer center_image"
                      >
                        <div class="flex justify-center w-[100%] h-[100%]">
                          <img
                            v-if="element.ParkSlotState == 1"
                            class="-rotate-90"
                            src="../../assets/imgs/top_view_motorbike_icon.png"
                            alt=""
                          />
                        </div>
                      </div>
                    </div>
                    <!-- End A3 -->
                    <!-- Begin A4 -->
                    <!-- <div class="motorbike__box mt-[18px]">
                      <div class="motorbike__slot--horizontal cursor-pointer center_image">
                        <div class="flex justify-center w-[100%] h-[100%]">
                          <img
                            v-if="element.ParkSlotState == 1"
                            class="-rotate-90"
                            src="../../assets/imgs/top_view_motorbike_icon.png"
                            alt=""
                          />
                        </div>
                      </div>
                    </div> -->
                    <!-- End A4 -->
                    <!-- Begin Cột D -->
                    <div class="flex flex-col justify-center items-center">
                      <div class="-rotate-90">Cột D</div>
                      <div class="column__icon mt-[12px]"></div>
                    </div>
                    <!-- End Cột D -->
                    <!-- Begin D1 -->
                    <div class="motorbike__box mt-[18px]">
                      <div
                        v-for="(element, index) in parkSlotData.D1"
                        @click="toggleGarageForm('1', element)"
                        class="motorbike__slot--horizontal cursor-pointer center_image"
                      >
                        <div class="flex justify-center w-[100%] h-[100%]">
                          <img
                            v-if="element.ParkSlotState == 1"
                            class="-rotate-90"
                            src="../../assets/imgs/top_view_motorbike_icon.png"
                            alt=""
                          />
                        </div>
                      </div>
                    </div>
                    <!-- End D1 -->
                    <!-- Begin D2 -->
                    <div class="motorbike__box mt-[18px]">
                      <div
                        v-for="(element, index) in parkSlotData.D2"
                        class="motorbike__slot--horizontal cursor-pointer center_image"
                        @click="toggleGarageForm('1', element)"
                      >
                        <div class="flex justify-center w-[100%] h-[100%]">
                          <img
                            v-if="element.ParkSlotState == 1"
                            class="-rotate-90"
                            src="../../assets/imgs/top_view_motorbike_icon.png"
                            alt=""
                          />
                        </div>
                      </div>
                    </div>
                    <!-- End D2 -->
                    <!-- Begin D3 Bikecycle -->
                    <div class="bikecycle__box flex mt-[18px]">
                      <div>
                        <div
                          v-for="(element, index) in parkSlotData.D3"
                          class="bikecycle__slot--horizontal center_image p-[4px]"
                          @click="
                            element.ParkSlotState == 0
                              ? toggleGarageForm('0', element)
                              : toggleGarageOutForm(null, element.ParkSlotCode)
                          "
                        >
                          <div
                            class="flex items-center justify-center w-[100%] h-[100%]"
                          >
                            <img
                              v-if="element.ParkSlotState == 1"
                              class="-rotate-90 h-[32px]"
                              src="../../assets/imgs/top_view_bikecycle_icon.png"
                              alt=""
                            />
                          </div>
                        </div>
                      </div>
                      <div>
                        <div
                          v-for="(element, index) in parkSlotData.D4"
                          class="bikecycle__slot--horizontal center_image p-[4px]"
                          @click="
                            element.ParkSlotState == 0
                              ? toggleGarageForm('0', element)
                              : toggleGarageOutForm(null, element.ParkSlotCode)
                          "
                        >
                          <div
                            class="flex items-center justify-center w-[100%] h-[100%]"
                          >
                            <img
                              v-if="element.ParkSlotState == 1"
                              class="-rotate-90 h-[32px]"
                              src="../../assets/imgs/top_view_bikecycle_icon.png"
                              alt=""
                            />
                          </div>
                        </div>
                      </div>
                    </div>
                    <!-- End D3 Bikecycle -->
                    <!-- <div class="invisible">Thang máy</div> -->
                  </div>
                </div>
                <div class="car__box2 flex">
                  <div
                    v-for="(element, index) in parkSlotData.F1"
                    @click="toggleGarageForm('2', element)"
                    class="car__slot cursor-pointer center_image"
                  >
                    <img
                      v-if="element.ParkSlotState == 1"
                      class="rotate-180"
                      src="../../assets/imgs/top_view_car_icon.png"
                      alt=""
                    />
                  </div>
                  <div class="w-[50px] flex flex-col items-center mx-[12px]">
                    <div class="">Cột F</div>
                    <div>
                      <div class="column__icon"></div>
                    </div>
                  </div>
                  <div
                    v-for="(element, index) in parkSlotData.F2"
                    class="car__slot cursor-pointer center_image"
                    @click="toggleGarageForm('2', element)"
                  >
                    <img
                      class="rotate-180"
                      v-if="element.ParkSlotState == 1"
                      src="../../assets/imgs/top_view_car_icon.png"
                      alt=""
                    />
                  </div>
                  <!-- <div class="w-[32px] h-[64px]"></div> -->
                </div>
              </div>
              <div class="ml-[10px] flex flex-col justify-between">
                <div class="w-[90px] h-[50px] elevator">
                  <div class="flex justify-center">Thang máy</div>
                </div>
                <div class="w-[90px] h-[50px] elevator flex items-end">
                  <div class="flex justify-center w-[100%]">Thang máy</div>
                </div>
              </div>
              <div class="ml-[12px] h-[100%] flex flex-col justify-between">
                <div class="flex">
                  <div
                    v-for="(element, index) in parkSlotData.G1"
                    @click="
                      element.ParkSlotState == 0
                        ? toggleGarageForm('2', element)
                        : toggleGarageOutForm()
                    "
                    class="car__slot cursor-pointer center_image"
                  >
                    <img
                      v-if="element.ParkSlotState == 1"
                      src="../../assets/imgs/top_view_car_icon.png"
                      alt=""
                    />
                  </div>
                  <div class="w-[50px] flex flex-col items-center mx-[12px]">
                    <div class="">Cột G</div>
                    <div>
                      <div class="column__icon"></div>
                    </div>
                  </div>
                  <div
                    v-for="(element, index) in parkSlotData.G2"
                    @click="
                      element.ParkSlotState == 0
                        ? toggleGarageForm('2', element)
                        : toggleGarageOutForm()
                    "
                    class="car__slot cursor-pointer center_image"
                  >
                    <img
                      v-if="element.ParkSlotState == 1"
                      src="../../assets/imgs/top_view_car_icon.png"
                      alt=""
                    />
                  </div>
                </div>
                <div class="flex">
                  <div
                    v-for="(element, index) in parkSlotData.H1"
                    @click="
                      element.ParkSlotState == 0
                        ? toggleGarageForm('2', element)
                        : toggleGarageOutForm()
                    "
                    class="car__slot cursor-pointer center_image"
                  >
                    <img
                      v-if="element.ParkSlotState == 1"
                      src="../../assets/imgs/top_view_car_icon.png"
                      alt=""
                    />
                  </div>
                  <div class="w-[50px] flex flex-col items-center mx-[12px]">
                    <div class="">Cột H</div>
                    <div>
                      <div class="column__icon"></div>
                    </div>
                  </div>
                  <div
                    v-for="(element, index) in parkSlotData.H2"
                    @click="
                      element.ParkSlotState == 0
                        ? toggleGarageForm('2', element)
                        : toggleGarageOutForm()
                    "
                    class="car__slot cursor-pointer center_image"
                  >
                    <img
                      class="rotate-180"
                      v-if="element.ParkSlotState == 1"
                      src="../../assets/imgs/top_view_car_icon.png"
                      alt=""
                    />
                  </div>
                </div>
              </div>
            </div>
            <div class="flex items-center" v-show="parkSlotsObject.A1">
              <div class="w-[100%] h-[70px] elevator rotate-90 px-[8px]">
                <div class="flex justify-center">Lối vào</div>
              </div>
            </div>
          </div>
          <!-- <div class="bikecycle__box flex">
            <w-menu right show-on-hover>
              <template #activator="{ on }">
                <div class="bikecycle__slot center_image p-[4px]" >
                  <div class="flex justify-center">
                    <img src="../../assets/imgs/bikecycle_icon.png" alt="" />
                  </div>
                </div>
              </template>
              <div>
                <div><span class="font-bold">Loại xe:</span> Xe đạp</div>
                <div><span class="font-bold">Biển số:</span> 19-L1 99999</div>
                <div><span class="font-bold">Tên chủ xe:</span></div>
                <div><span class="font-bold">Giờ vào:</span></div>
              </div>
            </w-menu>
          </div> -->
        </div>
      </div>
    </div>

    <!-- BEGIN: Form cho xe vào garage -->
    <DForm
      title="Thông tin xe vào bãi"
      v-show="isShowGarageForm"
      @onClose="toggleGarageForm"
    >
      <template #form_body>
        <!-- Form Header -->
        <div class="w-[1000px] flex">
          <div class="w-[40%]" v-if="parkingHistoryFormData.Vehicle > 0">
            <div class="my-[4px]">Nhập hình ảnh của xe:</div>
            <img
              class="max-h-[250px] my-[12px]"
              :src="licensePlateImageURL"
              alt=""
            />
            <input
              type="file"
              @input="handleImageChange"
              ref="licensePlateImageInput"
              accept="image/*"
            />
            <br />
            <DButton
              class="my-[4px]"
              @click="garageStore.getLicensePlateFromImage"
              >Lấy thông tin biển số</DButton
            >
          </div>
          <div v-else class="w-[40%]">
            <DInput
              label="Mã khách hàng hội viên (Nếu có)"
              v-model="bikecycleParkMemberCode"
            ></DInput>
            <DButton
              class="mt-[12px]"
              @click="
                garageStore.getParkMemberByParkMemberCodeAsync(
                  bikecycleParkMemberCode
                )
              "
              >Kiểm tra thông tin</DButton
            >
            <div>
              <div class="my-[4px]">Nhập hình ảnh của xe:</div>
              <img
                class="max-h-[250px] my-[12px]"
                :src="licensePlateImageURL"
                alt=""
              />
              <input
                type="file"
                @input="handleImageChange"
                ref="licensePlateImageInput3"
                accept="image/*"
              />
              <br />
              <!-- <DButton
                class="mt-[16px]"
                @click="garageStore.upLoadBikeCycleFormDataAsync"
                >Lưu thông tin hình ảnh xe vào bãi</DButton
              > -->
            </div>
          </div>
          <div class="pl-[16px] w-[60%] flex flex-col justify-between">
            <div>
              <div class="flex items-center my-[8px]">
                <div class="w-[150px] font-bold h-[24px] flex">Ảnh</div>
                <div class="h-[24px] flex">:</div>
                <img
                  class="h-[100px] rounded-[50%] ml-[2px]"
                  :src="parkMemberInf.AvatarLink"
                  alt=""
                />
              </div>
              <div class="my-[4px] flex">
                <div class="w-[150px] font-bold">Thông tin biển số</div>
                :
                {{
                  garageStore.convertDataToUIText(
                    parkingHistoryFormData.LicensePlate
                  )
                }}
              </div>
              <div class="flex">
                <div class="w-[150px] font-bold">Loại xe</div>
                :
                {{
                  garageStore.convertVehicleTypeToUIText(
                    parkingHistoryFormData.Vehicle
                  )
                }}
              </div>
              <div class="flex">
                <div class="w-[150px] font-bold">Loại vé</div>
                : {{ garageStore.convertTicketType(ticketType) }}
              </div>
              <!-- Thông tin khách hàng gửi xe -->
              <div>
                <div class="">
                  <div class="flex">
                    <div class="w-[150px] font-bold">Họ và tên</div>
                    :
                    {{
                      garageStore.convertDataToUIText(parkMemberInf.FullName)
                    }}
                  </div>
                  <div class="flex items-center">
                    <div class="w-[150px] font-bold">Địa chỉ</div>
                    :
                    {{ garageStore.convertDataToUIText(parkMemberInf.Address) }}
                  </div>

                  <div class="flex items-center">
                    <div class="w-[150px] font-bold">Số điện thoại</div>
                    :
                    {{ garageStore.convertDataToUIText(parkMemberInf.Mobile) }}
                  </div>
                  <div class="flex">
                    <div class="w-[150px] font-bold flex">Giá vé</div>
                    :
                    <div
                      class="ml-[2px]"
                      v-html="
                        garageStore.getKeepVehiclePriceHTML(
                          parkingHistoryFormData.Vehicle,
                          parkMemberInf.ParkMemberCode
                        )
                      "
                    ></div>
                    <!-- <div class="ml-[2px]">10.000đ/tiếng</div> -->
                  </div>
                </div>
              </div>
            </div>
            <div class="h-[36px] align-bottom">
              <DButton @click="enterVehicleToGarage">Thêm xe vào bãi</DButton>
            </div>
          </div>
        </div>
      </template>
    </DForm>
    <!-- END: Form cho xe vào garage -->

    <DForm
      title="Thông tin xe đang gửi tại bãi"
      v-show="isShowGarageOutForm"
      @onClose="toggleGarageOutForm"
    >
      <template #form_body>
        <div class="flex w-[900px]">
          <div class="w-[50%]">
            <div class="flex items-center">
              <div class="w-[150px] font-bold">Mã vị trí</div>
              : {{ parkingHistoryData.ParkSlotCode }}
            </div>
            <div class="flex items-center">
              <div class="w-[150px] font-bold">Loại phương tiện</div>
              :
              {{
                garageStore.convertVehicleTypeToUIText(
                  parkingHistoryData.Vehicle
                )
              }}
            </div>
            <div
              class="flex items-center"
              v-if="parkingHistoryData.LicensePlate"
            >
              <div class="w-[150px] font-bold">Biển số xe</div>
              :
              {{
                garageStore.convertDataToUIText(parkingHistoryData.LicensePlate)
              }}
            </div>
            <div class="flex items-center">
              <div class="w-[150px] font-bold">Thời gian vào</div>
              :
              {{
                garageStore.convertDateDBToCustomFormat(
                  parkingHistoryData.CreatedDate
                )
              }}
            </div>
            <div class="flex items-center">
              <div class="w-[150px] font-bold">Giá vé</div>
              :
              <div
                class="pl-[2px]"
                v-html="
                  garageStore.getKeepVehiclePriceHTML(
                    parkingHistoryData.Vehicle,
                    parkingHistoryData?.ParkMemberCode
                  )
                "
              ></div>
            </div>
          </div>
          <div class="w-[50%]" v-if="parkMemberInf.ParkMemberCode">
            <div class="flex">
              <div class="w-[200px] font-bold">Họ và tên khách hàng</div>
              :
              {{ garageStore.convertDataToUIText(parkMemberInf.FullName) }}
            </div>
            <div class="flex items-center">
              <div class="w-[200px] font-bold">Địa chỉ</div>
              :
              {{ garageStore.convertDataToUIText(parkMemberInf.Address) }}
            </div>

            <div class="flex items-center">
              <div class="w-[200px] font-bold">Số điện thoại</div>
              :
              {{ garageStore.convertDataToUIText(parkMemberInf.Mobile) }}
            </div>
          </div>
          <div v-else class="flex w-[50%]">
            <b class="block w-[250px]">Thông tin khách hàng: </b> Khách vãng lai
            không có thông tin trong hệ thống
          </div>
        </div>
        <div>
          <h1 class="mt-[16px] mb-[4px] text-[24px]">Hóa đơn gửi xe</h1>

          <div class="flex">
            <div class="w-[50%]">
              <div>
                <div class="my-[4px] font-bold">
                  {{
                    parkingHistoryData.Vehicle > 0
                      ? "Nhập hình ảnh của xe:"
                      : "Kiểm tra ảnh xe vào và xe ra:"
                  }}
                </div>
                <img
                  class="max-h-[250px] my-[12px]"
                  :src="licensePlateImageURL"
                  alt=""
                />
                <input
                  type="file"
                  @input="handleImageChange"
                  ref="licensePlateImageInput2"
                  accept="image/*"
                />
                <br />
                <div v-show="parkingHistoryData.Vehicle > 0">
                  <DButton
                    class="my-[4px]"
                    @click="garageStore.checkLicensePlateWhenVehicleOutAsync"
                    >Kiểm tra biển số xe ra</DButton
                  >
                  <div class="flex items-center">
                    <div class="w-[150px] mr-[2px]">Biển số xe ra:</div>
                    {{ outLicensePlate }}
                  </div>
                </div>
              </div>
              <div class="my-[12px]">
                <DDatePicker
                  ref="vehicleOutDatePicker"
                  label="Nhập thời gian xe ra"
                  required
                  focus
                  :enableTime="true"
                  v-model:date="parkingHistoryData.VehicleOutDateUI"
                  dateFormat="d/m/Y H:i"
                ></DDatePicker>
                <DButton @click="callInvoice" class="mt-[4px]"
                  >Tính tiền gửi xe</DButton
                >
              </div>
            </div>

            <div class="w-[50%] ml-[8px]">
              <div class="my-[12px]">
                <div v-if="parkMemberInf.VehicleInImageLink" class="">
                  <div class="w-[200px] font-bold">Hình ảnh xe vào bến :</div>
                  <img
                    class="max-h-[150px]"
                    :src="parkMemberInf.VehicleInImageLink"
                    alt="Hình ảnh xe đạp vào bến"
                  />
                </div>

                <h2 class="font-bold">Mã QR thanh toán</h2>
                <img alt="" :src="billUrl" class="h-[350px]" />
                <div v-if="billUrl">
                  Thời gian gửi:
                  {{
                    garageStore.convertDateDBToCustomFormat(
                      parkingHistoryData.CreatedDate
                    )
                  }}
                  -
                  {{
                    garageStore.convertDateDBToCustomFormat(
                      parkingHistoryData.VehicleOutDate
                    )
                  }}
                </div>
                <DButton v-show="billUrl" @click="enterVehicleOutGarage"
                  >Lấy xe ra khỏi bãi</DButton
                >
              </div>
            </div>
          </div>
        </div>
      </template>
    </DForm>
  </div>
</template>

<script setup>
import { useGarageStore } from "./GarageStore";
import axios from "axios";
import { ref, onMounted, nextTick } from "vue";
import { storeToRefs } from "pinia";
// Khởi tạo store
const garageStore = useGarageStore();
const {
  parkSlotsObject,
  imageFile,
  parkingHistoryFormData,
  parkingHistoryData,
  ticketType,
  parkMemberInf,
  isExecuteSuccess,
  outLicensePlate,
  isCheckLicensePlateBeforeOut,
  billUrl,
} = storeToRefs(garageStore);
const parkSlotData = ref(parkSlotsObject);

function handleImageChange(event) {
  garageStore.setImageFile(event.target.files[0]);
  licensePlateImageURL.value = URL.createObjectURL(event.target.files[0]);
  // console.log(event.target.files[0]);
}

const licensePlateImageURL = ref("");
const licensePlateImageInput = ref(null);
const licensePlateImageInput2 = ref(null);
const licensePlateImageInput3 = ref(null);
const isShowGarageForm = ref(false);
const isShowGarageOutForm = ref(false);
const bikecycleParkMemberCode = ref("");
const vehicleOutDatePicker = ref(null);
const garageFormData = ref({
  title: "Nhập thông tin xe vào bến!",
});

function toggleGarageForm(vehicleType, element) {
  isShowGarageForm.value = !isShowGarageForm.value;
  if (isShowGarageForm.value) {
    garageStore.isCheckLicensePlateBeforeOut = false;
    // console.log(parkSlotCode);
    garageStore.ticketType = 0;
    garageStore.parkingHistoryFormData.Vehicle = vehicleType;
    garageStore.parkingHistoryFormData.ParkSlotCode = element.ParkSlotCode;
  } else {
    garageStore.resetParkingHistoryFormData();
    garageStore.resetParkMemberInf();
    garageStore.setImageFile(null);
    licensePlateImageInput2.value = "";
    licensePlateImageInput3.value = "";
    licensePlateImageURL.value = "";
    licensePlateImageInput.value = "";
  }
}

async function toggleGarageOutForm(licensePlate, parkSlotCode) {
  isShowGarageOutForm.value = !isShowGarageOutForm.value;
  if (isShowGarageOutForm.value) {
    garageStore.isCheckLicensePlateBeforeOut = false;
    // garageStore.parkingHistoryData.VehicleOutDate =
    //   garageStore.getCurrentTimeString();
    if (licensePlate) {
      await garageStore.getParkingVehicleAsync(licensePlate);
    } else {
      await garageStore.getBikecycleInfInGarage(parkSlotCode);
    }
  } else {
    garageStore.resetParkingHistoryFormData();
    garageStore.resetParkMemberInf();
    garageStore.setImageFile(null);
    garageStore.parkingHistoryData.VehicleOutDate = "";
    billUrl.value = "";
    // vehicleOutDatePicker.value.date = null;
    console.log(vehicleOutDatePicker);
    licensePlateImageInput2.value = "";
    licensePlateImageInput3.value = "";
    licensePlateImageURL.value = "";
    licensePlateImageInput.value = "";
  }
}

async function enterVehicleToGarage() {
  // console.log(garageStore.parkingHistoryFormData.Vehicle);
  if (garageStore.parkingHistoryFormData.Vehicle > 0) {
    await garageStore.createParkingHistoryAsync();
  } else {
    await garageStore.upLoadBikeCycleFormDataAsync();
  }
  // console.log(isExecuteSuccess.value);
  if (isExecuteSuccess.value) {
    toggleGarageForm();
  }
}

async function enterVehicleOutGarage() {
  await garageStore.updateParkingHistoryAsync();
  console.log(isExecuteSuccess.value);
  if (isExecuteSuccess.value) {
    toggleGarageOutForm();
  }
}

async function callInvoice() {
  garageStore.invoice();
  await nextTick();
}

onMounted(async () => {
  await garageStore.getParkSlotByFloorAsync();
});
</script>

<style lang="scss" scoped>
.car__slot {
  width: 32px;
  height: 64px;
  padding: 4px;
  border-radius: var(--border-radius-general);
  background-color: #bdbdbd;
  margin-right: 4px;
}

// .motorbike__slot {
//   width: 48px;
//   height: 96px;
//   border-radius: var(--border-radius-general);
//   background-color: #bdbdbd;
//   margin-right: 4px;
// }

.motorbike__slot--horizontal {
  margin-top: 2px;
  margin-bottom: 2px;
  width: 48px;
  height: 32px;
  border-radius: var(--border-radius-general);
  background-color: #bdbdbd;
  margin-right: 4px;
}

.bikecycle__slot--horizontal {
  width: 40px;
  height: 24px;
  border-radius: var(--border-radius-general);
  background-color: #bdbdbd;
  margin-right: 4px;
  margin-top: 4px;
  margin-bottom: 3px;
}

.center_image {
  display: flex;
  justify-content: center;
  align-items: center;
}

.column__icon {
  border-radius: 50%;
  width: 12px;
  height: 12px;
  background: #000;
}

.elevator {
  border-style: dashed;
  border-color: #ccc; /* Màu sắc của border */

  /* Chỉ định border cho top, left và right */
  border-top-width: 0px;
  border-left-width: 2px;
  border-right-width: 2px;

  /* Không có border ở phía bottom */
  border-bottom-width: 0;
}
</style>
